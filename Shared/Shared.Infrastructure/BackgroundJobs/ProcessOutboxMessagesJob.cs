using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;
using Shared.Core.Common.Events;
using Shared.Core.Interfaces;
using Shared.Core.Outbox;

namespace Shared.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly IGeneralDbContext _context;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(IGeneralDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<OutboxMessage> messages = await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

                foreach (var message in messages)
                {
                    IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings
                    {
                        //TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    });
                    if (domainEvent is null)
                    {
                        continue;
                    }

                    AsyncRetryPolicy policy = Policy.Handle<Exception>().WaitAndRetryAsync(3 , 
                        attempt => TimeSpan.FromMicroseconds(attempt));

                    PolicyResult policyResult = await policy.ExecuteAndCaptureAsync(() =>
                    _publisher.Publish(domainEvent, context.CancellationToken));
                    message.Error = policyResult.FinalException?.ToString();
                    message.ProcessedOnUtc = DateTime.UtcNow;
                 
                    _context.OutboxMessages.Update(message);
                    await _context.SaveChangesAsync(context.CancellationToken);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

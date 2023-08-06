using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Core.Interfaces;
using Shared.Core.Outbox;
using Shared.Models.Core;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace Shared.Infrastructure.Interceptors
{
    public class BackgroundDomainEventSaveChangesInterceptor : SaveChangesInterceptor
    {

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            IGeneralDbContext generalDbContext = eventData.Context.GetService<IGeneralDbContext>();
            if (context == null)
                return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var entities = context.ChangeTracker
               .Entries<BaseEntity>()
               .Where(e => e.Entity.BackgroundDomainEvents.Any())
            .Select(e => e.Entity);



            var outboxMessages = entities
                .SelectMany(e => e.BackgroundDomainEvents)
                .Select(e => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccurredOnUtc = DateTime.UtcNow,
                    Type = e.GetType().Name,
                    Content = JsonConvert.SerializeObject(e, new JsonSerializerSettings
                    {
                        //TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.Objects,
                        NullValueHandling = NullValueHandling.Ignore,
                    }),
                    // Content = JsonConvert.Serialize(e,e.GetType()),
                })
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());
            await generalDbContext.OutboxMessages.AddRangeAsync(outboxMessages);
            int x = 0;
            if (outboxMessages.Count > 0 && x != 1)
            {
                x++;
                await generalDbContext.SaveChangesAsync(cancellationToken);

            }
            var res = await base.SavingChangesAsync(eventData, result, cancellationToken);
            return res;
        }
    }
}

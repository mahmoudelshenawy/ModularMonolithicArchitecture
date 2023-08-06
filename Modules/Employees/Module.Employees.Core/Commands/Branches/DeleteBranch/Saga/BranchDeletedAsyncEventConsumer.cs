using MassTransit;
using Microsoft.Extensions.Logging;

namespace Module.Employees.Core.Commands.Branches.DeleteBranch.Saga
{
    public class BranchDeletedAsyncEventConsumer : IConsumer<BranchDeletedAsyncEvent>
    {
        private readonly ILogger<BranchDeletedAsyncEventConsumer> _logger;

        public BranchDeletedAsyncEventConsumer(ILogger<BranchDeletedAsyncEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BranchDeletedAsyncEvent> context)
        {
            _logger.LogInformation("===========================================================>");
            _logger.LogInformation(context.Message.Message);
            return Task.CompletedTask;
        }
    }
}

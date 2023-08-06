using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Core.Common.EventBus;

namespace Module.Employees.Core.Commands.Branches.CreateBranch
{
    public sealed class BranchCreatedAsyncEventConsumer : IConsumer<BranchCreatedAsyncEvent>
    {
        private readonly ILogger<BranchCreatedAsyncEventConsumer> _logger;

        public BranchCreatedAsyncEventConsumer(ILogger<BranchCreatedAsyncEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<BranchCreatedAsyncEvent> context)
        {
            _logger.LogInformation("===============================================================================>");
            _logger.LogInformation(context.Message.ToString());
            return Task.CompletedTask;
        }
    }
}

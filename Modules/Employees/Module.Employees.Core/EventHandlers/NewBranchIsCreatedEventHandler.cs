using Microsoft.Extensions.Logging;
using Module.Employees.Core.Events;
using Shared.Core.Common.Events;

namespace Module.Employees.Core.EventHandlers
{
    public class NewBranchIsCreatedEventHandler : IDomainEventHandler<NewBranchIsCreatedEvent>
    {
        private readonly ILogger<NewBranchIsCreatedEventHandler> _logger;
        public NewBranchIsCreatedEventHandler(ILogger<NewBranchIsCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(NewBranchIsCreatedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Id == null) return Task.CompletedTask;

            _logger.LogInformation("new branch is created did you cofirm that admin?!");
            _logger.LogInformation($"branch id {notification.Id}");
            _logger.LogInformation($"{DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}

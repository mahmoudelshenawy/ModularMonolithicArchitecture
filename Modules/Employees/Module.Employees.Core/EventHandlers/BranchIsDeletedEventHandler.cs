using MediatR;
using Microsoft.Extensions.Logging;
using Module.Employees.Core.Events;
using Shared.Core.Common.Events;

namespace Module.Employees.Core.EventHandlers
{
    internal class BranchIsDeletedEventHandler : INotificationHandler<BranchIsDeletedEvent>
    {
        private readonly ILogger<BranchIsDeletedEventHandler> _logger;
        public BranchIsDeletedEventHandler(ILogger<BranchIsDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(BranchIsDeletedEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Id == null) return Task.CompletedTask;

            _logger.LogInformation("the branch is deleted did you cofirm that admin?!");
            _logger.LogInformation($"branch id {notification.Id}");
            _logger.LogInformation($"{DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}

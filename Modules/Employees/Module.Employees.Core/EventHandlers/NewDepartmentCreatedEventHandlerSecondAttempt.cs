using Microsoft.Extensions.Logging;
using Module.Employees.Core.Events;
using Shared.Core.Common.Events;

namespace Module.Employees.Core.EventHandlers
{
    internal class NewDepartmentCreatedEventHandlerSecondAttempt : IDomainEventHandler<NewDepartmentCreatedEvent>
    {
        private readonly ILogger<NewDepartmentCreatedEventHandlerSecondAttempt> _logger;

        public NewDepartmentCreatedEventHandlerSecondAttempt(ILogger<NewDepartmentCreatedEventHandlerSecondAttempt> logger)
        {
            _logger = logger;
        }

        public async Task Handle(NewDepartmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("-------------------------------------------->");
            _logger.LogInformation(notification.GetType().ToString());
            _logger.LogInformation($"{notification.GuidId} {notification.Id} the identifier");
            _logger.LogInformation(GetType().Name + "Is Running");
            _logger.LogInformation("-------------------------------------------->");
        }
    }
}

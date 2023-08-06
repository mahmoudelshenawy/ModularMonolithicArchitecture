using MediatR;
using Microsoft.Extensions.Logging;
using Module.Catalog.Core.Events;

namespace Module.Catalog.Core.EventHandlers
{
    public class NewProductAddedEventHandler : INotificationHandler<NewProductAddedEvent>
    {
        private readonly ILogger<NewProductAddedEventHandler> _logger;

        public NewProductAddedEventHandler(ILogger<NewProductAddedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(NewProductAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Product Catalog Module Domain Event: {DomainEvent}", notification.GetType().Name);
            _logger.LogInformation($"{notification._product.Name}");
            return Task.CompletedTask;
        }
    }
}

using MediatR;
using Shared.Models.Core;

namespace Shared.Core.Common.Events
{
    public interface IDomainEventHandler<DomainEvent> : INotificationHandler<DomainEvent>
        where DomainEvent : IDomainEvent
    {
    }
}

using MediatR;

namespace Shared.Core.Common.Events
{
    public interface IDomainEvent : INotification
    {
        Guid? GuidId { get; }
        int? Id { get; }
    }
}

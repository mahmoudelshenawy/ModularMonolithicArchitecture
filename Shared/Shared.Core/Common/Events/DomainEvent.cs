using Shared.Models.Core;

namespace Shared.Core.Common.Events
{
    public abstract record DomainEvent(Guid? GuidId , int? Id) : IDomainEvent;
}

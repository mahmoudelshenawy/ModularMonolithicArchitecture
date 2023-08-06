using Shared.Core.Common.Events;

namespace Module.Employees.Core.Events
{
    public record NewDepartmentCreatedEvent(Guid? GuidId, int? Id) : DomainEvent(GuidId, Id);
}

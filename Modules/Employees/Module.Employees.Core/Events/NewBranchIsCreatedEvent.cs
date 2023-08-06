using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Shared.Core.Common.Events;
using Shared.Models.Core;

namespace Module.Employees.Core.Events
{
    public sealed record NewBranchIsCreatedEvent(Guid? GuidId , int? Id) : DomainEvent(GuidId , Id);
}

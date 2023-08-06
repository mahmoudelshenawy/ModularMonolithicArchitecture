using Shared.Core.Common.Events;
using Shared.Models.Core;

namespace Module.Employees.Core.Events
{
    public record BranchIsDeletedEvent(Guid? GuidId, int? Id) : BaseEvent;
   
}

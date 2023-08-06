using Shared.Models.Core;

namespace Module.Employees.Core.Entities.Payroll
{
    public abstract class BaseEmployeeId : BaseEntity
    {
        public Guid EmployeeId { get; set; } = Guid.NewGuid();
        public Employee Employee { get; set; }
    }
}

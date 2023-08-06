using Shared.Models.Core;

namespace Module.Employees.Core.Entities.Payroll
{
    public class PayrollTypes : BaseEntity
    {
        private PayrollTypes(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty!;

        private static PayrollTypes Create(string name) => new PayrollTypes(name);
    }
}

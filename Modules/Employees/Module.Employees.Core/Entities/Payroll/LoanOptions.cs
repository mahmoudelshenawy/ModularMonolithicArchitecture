using Shared.Models.Core;

namespace Module.Employees.Core.Entities.Payroll
{
    public class LoanOptions : BaseEntity
    {
        private LoanOptions(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty!;

        public static LoanOptions Create(string name) => new LoanOptions(name);
    }
}

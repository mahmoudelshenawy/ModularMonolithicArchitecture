using Shared.Models.Core;

namespace Module.Employees.Core.Entities.Payroll
{
    public class AllowanceOptions : BaseEntity
    {
        public string Name { get; private set; } = string.Empty!;

        private AllowanceOptions(string name)
        {
            Name = name;
        }
        public static AllowanceOptions Create(string name)
        {
            return new AllowanceOptions(name);
        }
    }
}

using Shared.Models.Core;

namespace Module.Employees.Core.Entities.Payroll
{
    public class DeductionOptions : BaseEntity
    {
        private DeductionOptions(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty!;

        public static DeductionOptions Create(string name) => new DeductionOptions(name);

    }
}

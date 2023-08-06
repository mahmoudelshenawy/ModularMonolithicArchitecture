using Module.Employees.Core.Errors;
using Shared.Models.Core;

namespace Module.Employees.Core.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; private set; }
        public virtual List<Employee> Employees { get; private set; } = new();
        public Branch()
        {
            
        }
        private Branch(string name)
        {
            Name = name;
        }

        public static Branch Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("name should not be empty");
            }
            return new Branch(name);
        }
        public static Branch Update(Branch branch, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("name should not be empty");
            }
            branch.Name = name;
            return branch;
        }
    }
}

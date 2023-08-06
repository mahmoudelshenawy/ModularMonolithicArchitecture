using Shared.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Employees.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; private set; }
        public int BranchId { get; private set; }
        public Branch Branch { get; private set; }
        public virtual List<Employee> Employees { get; private set; } = new();

        private Department(string name, int branchId)
        {
            Name = name;
            BranchId = branchId;
        }

        public static Department Create(string name, int branchId)
        {
            return new Department(name, branchId);
        }

        public static Department Update(Department department, string name, int branchId)
        {
            if (department.BranchId != branchId)
                department.BranchId = branchId;
            if (!department.Name.Equals(name))
                department.Name = name;

            return department;
        }
    }
}

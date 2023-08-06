using Shared.Models.Models;

namespace Module.Employees.Core.Errors
{
    public static class DomainErrors
    {
        public static class Employee
        {
            public static readonly Error BasicSalaryIsNotDefined = new Error("Employee.UndefinedBasicSalary", @"The basic salary of the
             is not defined yet!.");
        }
    }
}

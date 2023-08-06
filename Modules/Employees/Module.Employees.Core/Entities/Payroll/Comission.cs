namespace Module.Employees.Core.Entities.Payroll
{
    public class Comission : BaseEmployeeId
    {
        public string Title { get; private set; }
        public decimal Amount { get; private set; }

        private Comission(Guid employeeId, string title , decimal amount)
        {
            EmployeeId = employeeId;
            Title = title;
            Amount = amount;
        }

        public static Comission Create(Guid employeeId, string title, decimal amount)
        {
            return new Comission(employeeId,title, amount);
        }
    }
}

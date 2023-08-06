namespace Module.Employees.Core.Entities.Payroll
{
    public class Allowance : BaseEmployeeId
    {
        public int AllowanceOptionId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public AllowanceOptions? AllowanceOption { get; set; }

        private Allowance(Guid employeeId, int allowanceOptionId, string title, decimal amount)
        {
            EmployeeId = employeeId;
            AllowanceOptionId = allowanceOptionId;
            Title = title;
            Amount = amount;
        }
        public Allowance Create(Guid employeeId, int allowanceOptionId, string title, decimal amount)
        {
            return new Allowance(employeeId,allowanceOptionId, title, amount);
        }
    }

    
}

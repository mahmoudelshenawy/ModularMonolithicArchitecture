namespace Module.Employees.Core.Entities.Payroll
{
    public class Deduction : BaseEmployeeId
    {
        public int DeductionOptionId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public DeductionOptions DeductionOption { get; set; }


        private Deduction(Guid employeeId, int deductionOptionId, string title, decimal amount)
        {
            EmployeeId = employeeId;
            DeductionOptionId = deductionOptionId;
            Title = title;
            Amount = amount;
        }

        public static Deduction Create(Guid employeeId, int deductionOptionId, string title, decimal amount)
        {
            return new Deduction(employeeId,deductionOptionId, title, amount);
        }
    }
}

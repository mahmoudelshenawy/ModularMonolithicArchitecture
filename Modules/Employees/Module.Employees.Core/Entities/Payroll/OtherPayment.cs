namespace Module.Employees.Core.Entities.Payroll
{
    public class OtherPayment : BaseEmployeeId
    {
        private OtherPayment(Guid employeeId, string title, decimal amount)
        {
            EmployeeId = employeeId;
            Title = title;
            Amount = amount;
        }

        public string Title { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }

        public static OtherPayment Create(Guid employeeId, string title, decimal amount) => new OtherPayment(employeeId,title, amount);
    }
}

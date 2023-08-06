namespace Module.Employees.Core.Entities.Payroll
{
    public class Loan : BaseEmployeeId
    {
        private Loan(Guid employeeId, int loanOptionId, string title, decimal amount, DateTime? startDate,
            DateTime? endDate, string reason)
        {
            EmployeeId = employeeId;
            LoanOptionId = loanOptionId;
            Title = title;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public int LoanOptionId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Reason { get; private set; } = string.Empty;

        public LoanOptions LoanOption { get; set; }

        public static Loan Create(Guid employeeId, int loanOptionId, string title, decimal amount, DateTime? startDate,
            DateTime? endDate, string reason)
        {
            return new Loan(employeeId,loanOptionId, title, amount, startDate, endDate, reason);
        }
    }
}

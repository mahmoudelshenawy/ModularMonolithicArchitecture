namespace Module.Employees.Core.Entities.Payroll
{
    public class Overtime : BaseEmployeeId
    {
        private Overtime(Guid employeeId,string title, int? days, int hours, decimal rate, decimal amount)
        {
            EmployeeId = employeeId;
            Title = title;
            Days = days;
            Hours = hours;
            Rate = rate;
            Amount = amount;
        }

        public string Title { get; private set; } = string.Empty;
        public int? Days { get; private set; }
        public int Hours { get; private set; } = 0; //hours or days
        public decimal Rate { get; private set; }
        public decimal Amount { get; private set; }

        public static Overtime Create(Guid employeeId, string title, int? days, int hours, decimal rate, decimal amount)
        {
            return new Overtime(employeeId, title, days, hours, rate, amount);
        }
    }
}

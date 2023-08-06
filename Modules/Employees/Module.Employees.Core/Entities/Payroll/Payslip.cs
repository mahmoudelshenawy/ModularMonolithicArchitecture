using Module.Employees.Core.Enums;

namespace Module.Employees.Core.Entities.Payroll
{
    public class Payslip : BaseEmployeeId
    {
        public Payslip(Guid employeeId, int? payrollTypeId, decimal netSalary, string salaryMonth, decimal basicSalary,
            PayslipStatus status, decimal? allowance, decimal? commission, decimal? loan, decimal? deduction,
            decimal? otherPayment, decimal? overtime)
        {
            EmployeeId = employeeId;
            PayrollTypeId = payrollTypeId;
            NetSalary = netSalary;
            SalaryMonth = salaryMonth;
            BasicSalary = basicSalary;
            Status = status;
            Allowance = allowance;
            Commission = commission;
            Loan = loan;
            Deduction = deduction;
            OtherPayment = otherPayment;
            Overtime = overtime;
        }
        public int? PayrollTypeId { get; private set; }
        public decimal NetSalary { get; private set; }
        public string SalaryMonth { get; private set; }
        public decimal BasicSalary { get; private set; }
        public PayslipStatus Status { get; private set; }
        public decimal? Allowance { get; private set; }
        public decimal? Commission { get; private set; }
        public decimal? Loan { get; private set; }
        public decimal? Deduction { get; private set; }
        public decimal? OtherPayment { get; private set; }
        public decimal? Overtime { get; private set; }

        public PayrollTypes PayrollType { get; set; }
        public static Payslip Create(Guid employeeId,int? payrollTypeId, decimal netSalary, string salaryMonth, decimal basicSalary,
            PayslipStatus status, decimal? allowance, decimal? commission, decimal? loan, decimal? deduction,
            decimal? otherPayment, decimal? overtime) => new Payslip(employeeId,payrollTypeId, netSalary, salaryMonth, basicSalary, status,
                allowance, commission, loan, deduction, otherPayment, overtime
                );
    }
}

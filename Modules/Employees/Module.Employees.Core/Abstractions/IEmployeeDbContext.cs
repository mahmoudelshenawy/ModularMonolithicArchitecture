using Microsoft.EntityFrameworkCore;
using Module.Employees.Core.Entities;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Core.Abstractions
{
    public interface IEmployeeDbContext 
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AllowanceOptions> AllowanceOptions { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionOptions> DeductionOptions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanOptions> LoanOptions { get; set; }
        public DbSet<OtherPayment> OtherPayments { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<PayrollTypes> PayrollTypes { get; set; }
        public DbSet<Payslip> Payslips { get; set; }

        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

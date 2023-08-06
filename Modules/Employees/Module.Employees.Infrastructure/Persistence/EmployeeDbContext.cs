using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Entities;
using Module.Employees.Core.Entities.Payroll;
using Shared.Core.Interfaces;
using Shared.Infrastructure.Interceptors;
using Shared.Infrastructure.Persistence;

namespace Module.Employees.Infrastructure.Persistence
{
    public class EmployeeDbContext : ModuleDbContext, IEmployeeDbContext
    {
        protected override string Schema => "Employee";
        public EmployeeDbContext(DbContextOptions options,
            ICurrentUserService currentUserService, IMediator mediator,
            BackgroundDomainEventSaveChangesInterceptor backgroundDomainEventSaveChangesInterceptor) 
            : base(options, currentUserService, mediator , backgroundDomainEventSaveChangesInterceptor)
        {
        }

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
    }
}

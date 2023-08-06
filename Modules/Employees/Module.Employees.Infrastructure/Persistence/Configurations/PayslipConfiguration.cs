using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class PayslipConfiguration : IEntityTypeConfiguration<Payslip>
    {
        public void Configure(EntityTypeBuilder<Payslip> builder)
        {
            builder.Property(b => b.BasicSalary).HasPrecision(10, 2);
            builder.Property(b => b.NetSalary).HasPrecision(10, 2);
            builder.Property(b => b.Allowance).HasPrecision(10, 2);
            builder.Property(b => b.Commission).HasPrecision(10, 2);
            builder.Property(b => b.Deduction).HasPrecision(10, 2);
            builder.Property(b => b.Loan).HasPrecision(10, 2);
            builder.Property(b => b.OtherPayment).HasPrecision(10, 2);
            builder.Property(b => b.Overtime).HasPrecision(10, 2);
            builder.Property(b => b.Status).HasConversion<string>();
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(c => c.EmployeeId);
            builder.HasOne(b => b.PayrollType).WithMany().HasForeignKey(c => c.PayrollTypeId);
        }
    }
}

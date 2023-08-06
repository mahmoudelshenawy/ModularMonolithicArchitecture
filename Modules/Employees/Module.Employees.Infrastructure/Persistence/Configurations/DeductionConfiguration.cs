using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class DeductionConfiguration : IEntityTypeConfiguration<Deduction>
    {
        public void Configure(EntityTypeBuilder<Deduction> builder)
        {
            builder.Property(b => b.Amount).HasPrecision(10, 2);
            builder.Property(b => b.Title).HasMaxLength(200);
            builder.HasOne(b => b.DeductionOption).WithMany().HasForeignKey(c => c.DeductionOptionId);
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(c => c.EmployeeId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class OvertimeConfiguration : IEntityTypeConfiguration<Overtime>
    {
        public void Configure(EntityTypeBuilder<Overtime> builder)
        {
            builder.Property(b => b.Amount).HasPrecision(10, 2);
            builder.Property(b => b.Rate).HasPrecision(10, 2);
            builder.Property(b => b.Title).HasMaxLength(200);
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(c => c.EmployeeId);
        }
    }
}

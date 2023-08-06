using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class OtherPaymentConfiguration : IEntityTypeConfiguration<OtherPayment>
    {
        public void Configure(EntityTypeBuilder<OtherPayment> builder)
        {
            builder.Property(b => b.Amount).HasPrecision(10, 2);
            builder.Property(b => b.Title).HasMaxLength(200);
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(c => c.EmployeeId);
        }
    }
}

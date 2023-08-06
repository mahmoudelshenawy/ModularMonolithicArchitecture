using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class AllowanceOptionsConfiguration : IEntityTypeConfiguration<AllowanceOptions>
    {
        public void Configure(EntityTypeBuilder<AllowanceOptions> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
        }
    }
}

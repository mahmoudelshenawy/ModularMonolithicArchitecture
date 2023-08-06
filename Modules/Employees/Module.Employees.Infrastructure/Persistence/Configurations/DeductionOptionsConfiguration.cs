using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class DeductionOptionsConfiguration : IEntityTypeConfiguration<DeductionOptions>
    {
        public void Configure(EntityTypeBuilder<DeductionOptions> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Employees.Core.Entities.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    public class LoanOptionsConfiguration : IEntityTypeConfiguration<LoanOptions>
    {
        public void Configure(EntityTypeBuilder<LoanOptions> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
        }
    }
}

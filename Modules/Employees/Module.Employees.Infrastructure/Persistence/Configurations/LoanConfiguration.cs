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
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.Property(b => b.Amount).HasPrecision(10, 2);
            builder.Property(b => b.Title).HasMaxLength(200);
            builder.HasOne(b => b.LoanOption).WithMany().HasForeignKey(c => c.LoanOptionId);
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(c => c.EmployeeId);
        }
    }
}

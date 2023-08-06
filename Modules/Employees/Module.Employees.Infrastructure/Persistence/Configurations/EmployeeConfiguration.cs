using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module.Employees.Core.Entities;
using Module.Employees.Core.Enums;

namespace Module.Employees.Infrastructure.Persistence.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.HasOne(b => b.Branch).WithMany().HasForeignKey(x => x.BranchId);
            //builder.HasOne(b => b.Department).WithMany().HasForeignKey(x => x.DepartmentId);
            var conversion = new EnumToStringConverter<EmployeeStatus>();

            builder.OwnsOne(p => p.AccountInformation, o =>
            {
                o.Property(p => p.AccountHolderName).HasMaxLength(250).HasColumnName("AccountHolderName");
                o.Property(p => p.AccountNumber).HasMaxLength(250).HasColumnName("AccountNumber");
                o.Property(p => p.BankName).HasMaxLength(250).HasColumnName("BankName");
                o.Property(p => p.BranchLocation).HasMaxLength(250).HasColumnName("BranchLocation");
                o.Property(p => p.BaseSalary).HasPrecision(10, 2).HasColumnName("BaseSalary");
            });

            builder.OwnsOne(p => p.CompanyInformation, b =>
            {
                b.Property(p => p.WorkHours).HasDefaultValue(8).HasColumnName("WorkHours");
              //  b.Property(p => p.BranchId).HasColumnName("BranchId");
              //  b.Property(p => p.DepartmentId).HasColumnName("DepartmentId");
                b.Property(p => p.DateOfHiring).HasColumnName("DateOfHiring");
            });
            builder.OwnsOne(p => p.PersonalInformation, b =>
            {
                b.Property(x => x.Name).HasMaxLength(250).HasColumnName("Name");
                b.Property(x => x.Phone).HasMaxLength(250).HasColumnName("Phone");
                b.Property(x => x.Address).HasMaxLength(250).HasColumnName("Address");
                b.Property(x => x.Age).HasMaxLength(250).HasColumnName("Age");
                b.Property(x => x.DateOfBirth).HasMaxLength(250).HasColumnName("DateOfBirth");
                b.Property(x => x.Gender).HasMaxLength(250).HasColumnName("Gender");
                b.Property(x => x.UserId).HasMaxLength(250).HasColumnName("UserId");
            });

            builder.Property(x => x.Status).HasConversion(conversion);
            builder.HasOne(x => x.ReportsToManager).WithMany().HasForeignKey(x => x.ReportsTo);
            
        }
    }
}

using Module.Employees.Core.Dtos;
using Module.Employees.Core.Enums;
using Shared.Models.Core;
using System.ComponentModel.DataAnnotations;

namespace Module.Employees.Core.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public new Guid Id { get; private set; } = Guid.NewGuid();
        public PersonalInformation PersonalInformation { get; private set; }
        public CompanyInformation CompanyInformation { get; private set; }
        public AccountInformation AccountInformation { get; private set; }
        public EmployeeStatus Status { get; private set; } = EmployeeStatus.Active;
        public string? Notes { get; private set; }
        public Guid? ReportsTo { get; private set; }
        public int BranchId { get; private set; }
        public int DepartmentId { get; private set; }

        //Navigation
        public Employee? ReportsToManager { get; set; }
        public Department Department { get; set; }
        public Branch Branch { get; set; }


        public static Employee Create(EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                PersonalInformation = PersonalInformation.Create(dto.UserId, dto.Name, dto.Age, dto.Gender, dto.Phone, dto.Address,
                dto.DateOfBirth),
                CompanyInformation = CompanyInformation.Create(dto.WorkHours, dto.DateOfHiring),
                AccountInformation = AccountInformation.Create(dto.AccountHolderName, dto.AccountNumber
                , dto.BankName, dto.BranchLocation, dto.BaseSalary),
                Status = Enum.Parse<EmployeeStatus>(dto.Status),
                Notes = dto.Notes,
                ReportsTo = dto.ReportsTo,
                BranchId = dto.BranchId,
                DepartmentId = dto.DepartmentId
            };
        }
        public static Employee Update(Employee employee, EmployeeDto dto)
        {
            employee.PersonalInformation = PersonalInformation.Update(employee.PersonalInformation,
                dto.UserId, dto.Name, dto.Age, dto.Gender, dto.Phone, dto.Address, dto.DateOfBirth);
            employee.CompanyInformation = CompanyInformation.Update(employee.CompanyInformation, dto.WorkHours, dto.DateOfHiring);
            employee.AccountInformation = AccountInformation.Update(employee.AccountInformation,dto.AccountHolderName, dto.AccountNumber
            , dto.BankName, dto.BranchLocation, dto.BaseSalary);
            employee.Status = dto.Status != null ? Enum.Parse<EmployeeStatus>(dto.Status) : employee.Status;
            employee.Notes = dto.Notes != null ? dto.Notes : employee.Notes;
            employee.ReportsTo = dto.ReportsTo != null ? dto.ReportsTo : employee.ReportsTo;
            employee.BranchId = dto.BranchId != 0 ? dto.BranchId : employee.BranchId;
            employee.DepartmentId = dto.DepartmentId != 0 ? dto.DepartmentId : employee.DepartmentId;
            return employee;
        }
        public static Employee AddUserIdToEmployee(Employee employee, string userId)
        {
            employee.PersonalInformation.updateEmployeeUserId(userId);
            return employee;
        }
    }
    public class AccountInformation
    {
        public string? AccountHolderName { get; private set; }
        public string? AccountNumber { get; private set; }
        public string? BankName { get; private set; }
        public string? BranchLocation { get; private set; }
        public decimal BaseSalary { get; private set; } = 0.00m;

        public AccountInformation() { }
        private AccountInformation(string? accountHolderName, string? accountNumber, string? bankName, string? branchLocation
            , decimal baseSalary)
        {
            AccountHolderName = accountHolderName;
            AccountNumber = accountNumber;
            BankName = bankName;
            BranchLocation = branchLocation;
            BaseSalary = baseSalary;
        }
        public static AccountInformation Create(string? accountHolderName, string? accountNumber, string? bankName, string? branchLocation
            , decimal baseSalary)
        {
            return new AccountInformation(accountHolderName, accountNumber, bankName, branchLocation, baseSalary);
        }
        public static AccountInformation Update(AccountInformation currentAccInfo,
            string? accountHolderName, string? accountNumber, string? bankName, string? branchLocation, decimal baseSalary)
        {
            return new AccountInformation
            {
                AccountHolderName = accountHolderName ?? currentAccInfo.AccountHolderName,
                AccountNumber = accountNumber ?? currentAccInfo.AccountNumber,
                BankName = bankName ?? currentAccInfo.BankName,
                BaseSalary = baseSalary == 0 ? currentAccInfo.BaseSalary : baseSalary,
                BranchLocation = branchLocation ?? currentAccInfo.BranchLocation
            };
        }

    }
    public class CompanyInformation
    {
        public int WorkHours { get; private set; }
        public DateTime? DateOfHiring { get; private set; }


        public CompanyInformation() { }
        private CompanyInformation(int workHours, DateTime? dateOfHiring)
        {
            WorkHours = workHours;
            DateOfHiring = dateOfHiring;
        }
        public static CompanyInformation Create(int workHours, DateTime? dateOfHiring)
        {
            return new CompanyInformation(workHours, dateOfHiring);
        }
        public static CompanyInformation Update(CompanyInformation currentCInfo, int? workHours, DateTime? dateOfHiring)
        {
            return new CompanyInformation
            {
                DateOfHiring = dateOfHiring ?? currentCInfo.DateOfHiring,
                WorkHours = workHours ?? currentCInfo.WorkHours
            };
        }

    }
    public class PersonalInformation
    {
        public string UserId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty!;
        public int? Age { get; private set; }
        public string? Gender { get; private set; }
        public string? Phone { get; private set; }
        public string? Address { get; private set; }

        public DateTime? DateOfBirth { get; private set; }
        public PersonalInformation() { }

        public void updateEmployeeUserId(string userId) => UserId = userId;

        private PersonalInformation(string userId, string name, int? age, string? gender, string? phone, string? address,
            DateTime? dateOfBirth)
        {
            UserId = userId;
            Name = name;
            Age = age;
            Gender = gender;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
        }

        public static PersonalInformation Create(string userId, string name, int? age, string? gender, string? phone, string? address,
            DateTime? dateOfBirth)
        {
            return new PersonalInformation(userId, name, age, gender, phone, address, dateOfBirth);
        }
        public static PersonalInformation Update(PersonalInformation currentPInfo,
            string? userId, string name, int? age, string? gender, string? phone, string? address, DateTime? dateOfBirth)
        {
            return new PersonalInformation
            {
                UserId = userId ?? currentPInfo.UserId,
                Name = name ?? currentPInfo.Name,
                Age = age ?? currentPInfo.Age,
                Gender = gender ?? currentPInfo.Gender,
                Phone = phone ?? currentPInfo.Phone,
                Address = address ?? currentPInfo.Address,
                DateOfBirth = dateOfBirth ?? currentPInfo.DateOfBirth
            };
        }

    }
}

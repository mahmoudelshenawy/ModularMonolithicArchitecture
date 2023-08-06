using AutoMapper;
using Module.Employees.Core.Entities;

namespace Module.Employees.Core.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public bool AllowLogin { get;  set; }
        public string UserId { get;  set; } = string.Empty;
        public string Name { get;  set; } = string.Empty!;
        public int? Age { get;  set; }
        public string? Gender { get;  set; }
        public string? Phone { get;  set; }
        public string? Address { get;  set; }

        public DateTime? DateOfBirth { get;  set; }
        public string Email { get;  set; } = string.Empty;
        public string Password { get;  set; } = string.Empty;
        public int BranchId { get;  set; }
        public int DepartmentId { get;  set; }
        public int WorkHours { get;  set; }
        public DateTime? DateOfHiring { get;  set; }

        public string? AccountHolderName { get;  set; }
        public string? AccountNumber { get;  set; }
        public string? BankName { get;  set; }
        public string? BranchLocation { get;  set; }
        public decimal BaseSalary { get;  set; } = 0.00m;

        public string? Status { get; set; }
        public string? Notes { get;  set; }
        public Guid? ReportsTo { get;  set; }
    }


    public class EmployeeDtoMapping : Profile
    {
        public EmployeeDtoMapping() => CreateMap<EmployeeDto, Employee>()
            .ForPath(dest => dest.PersonalInformation.Address , src => src.MapFrom(s => s.Address))
            .ForPath(dest => dest.PersonalInformation.Age , src => src.MapFrom(s => s.Age))
            .ForPath(dest => dest.PersonalInformation.DateOfBirth , src => src.MapFrom(s => s.DateOfBirth))
            .ForPath(dest => dest.PersonalInformation.Gender , src => src.MapFrom(s => s.Gender))
            .ForPath(dest => dest.PersonalInformation.Name , src => src.MapFrom(s => s.Name))
            .ForPath(dest => dest.PersonalInformation.Phone , src => src.MapFrom(s => s.Phone))
            .ForPath(dest => dest.PersonalInformation.UserId , src => src.MapFrom(s => s.UserId))
            .ForPath(dest => dest.CompanyInformation.DateOfHiring , src => src.MapFrom(s => s.DateOfHiring))
            .ForPath(dest => dest.CompanyInformation.WorkHours , src => src.MapFrom(s => s.WorkHours))
            .ForPath(dest => dest.AccountInformation.AccountHolderName , src => src.MapFrom(s => s.AccountHolderName))
            .ForPath(dest => dest.AccountInformation.AccountNumber , src => src.MapFrom(s => s.AccountNumber))
            .ForPath(dest => dest.AccountInformation.BankName , src => src.MapFrom(s => s.BankName))
            .ForPath(dest => dest.AccountInformation.BaseSalary , src => src.MapFrom(s => s.BaseSalary))
            .ForPath(dest => dest.AccountInformation.BranchLocation , src => src.MapFrom(s => s.BranchLocation))
            .ForMember(dest => dest.Status , src => src.MapFrom(s => s.Status))
            .ReverseMap();
        
    }
}

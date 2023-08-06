using AutoMapper;
using Module.Employees.Core.Entities;

namespace Module.Employees.Core.Dtos
{
    public class DepartmentDto
    {
        public DepartmentDto(int id, string name, int branchId)
        {
            Id = id;
            Name = name;
            BranchId = branchId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }

        public static DepartmentDto Create(int id, string name, int branchId) => new DepartmentDto(id, name, branchId);

        
    }

    public class DepartmentDtoMapping : Profile
    {
        public DepartmentDtoMapping() => CreateMap<DepartmentDto, Department>().ReverseMap();
         
       
    }
}

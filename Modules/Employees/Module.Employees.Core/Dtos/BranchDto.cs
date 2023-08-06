using AutoMapper;
using Module.Employees.Core.Entities;

namespace Module.Employees.Core.Dtos
{
    public class BranchDto
    {
        public BranchDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public static BranchDto Create(int id, string name) => new(id, name);
    }

    public class BranchDtoMapping : Profile
    {
        public BranchDtoMapping() => CreateMap<BranchDto, Branch>().ReverseMap();
        
    }
}

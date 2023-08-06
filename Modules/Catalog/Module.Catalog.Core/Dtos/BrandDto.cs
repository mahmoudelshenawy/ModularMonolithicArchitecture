using AutoMapper;
using Module.Catalog.Core.Entities;
using Shared.Core.Common;

namespace Module.Catalog.Core.Dtos
{
    public class BrandDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public override bool Success => Id != 0 ? true : false;

    }

    public class BrandMapping : Profile
    {
        public BrandMapping() => CreateMap<Brand, BrandDto>().ReverseMap();

    }


}

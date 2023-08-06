using AutoMapper;
using Module.Catalog.Core.Entities;
using Shared.Core.Common;

namespace Module.Catalog.Core.Dtos
{
    public class SupplierDto : BaseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? ModifiedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public override bool Success => Id != 0 ? true : false;
    }

    public class SupplierMapping : Profile
    {
        public SupplierMapping() => CreateMap<Supplier, SupplierDto>().ReverseMap();
       
    }
}

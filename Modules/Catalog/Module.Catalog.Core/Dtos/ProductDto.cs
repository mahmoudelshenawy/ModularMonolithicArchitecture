using AutoMapper;
using Module.Catalog.Core.Entities;
using Module.Catalog.Core.Enums;
using Shared.Core.Common;

namespace Module.Catalog.Core.Dtos
{
    public class ProductDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public ProductType Type { get; set; }
        public bool EnableStock { get; set; }
        public int AlertQuantity { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public override bool Success => Id != 0 ? true : false;
    }

    public class ProductMapping : Profile
    {
        public ProductMapping() => CreateMap<ProductDto, Product>().ReverseMap();

    }
}

//addproduct
//aproveproduct
//addstock
//updateproduct
//deleteproduct
//removeproducttemporarly
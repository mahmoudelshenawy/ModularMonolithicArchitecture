using Module.Catalog.Core.Enums;
using Shared.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Entities
{
    public class Product : BaseEntity
    {
        [StringLength(2500)]
        public string Name { get; set; }
        public string Sku { get; set; } = Guid.NewGuid().ToString();
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
        public Supplier Supplier { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
    }
}

using Shared.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Entities
{
    public class Supplier : BaseEntity
    {
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(100)]
        public string ContactName { get; set; }
        [StringLength(20)]
        public string Country { get; set; } = string.Empty;
        [StringLength(20)]
        public string City { get; set; } = string.Empty;
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}

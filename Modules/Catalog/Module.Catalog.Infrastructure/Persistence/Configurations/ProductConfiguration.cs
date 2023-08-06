using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Module.Catalog.Core.Entities;
using Module.Catalog.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var converter = new EnumToStringConverter<ProductType>();
            builder.Property(x => x.Type).HasConversion(converter);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Sku).HasMaxLength(200);
            builder.Property(x => x.SellingPrice).HasPrecision(10,2);
            builder.Property(x => x.PurchasePrice).HasPrecision(10,2);

            //builder.Property(x => x.Type).HasConversion(b => b.ToString(), v => Enum.Parse<ProductType>(v));
        }
    }
}

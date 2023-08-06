using FluentValidation;
using Module.Catalog.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Validators
{
    internal class ProductStockDtoValidator : AbstractValidator<ProductStockDto>
    {
        public ProductStockDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Increase).ExclusiveBetween(true , false);
        }
    }
}

using FluentValidation;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using Module.Catalog.Core.Enums;
using Module.Catalog.Shared.Interfaces;
using Module.Catalog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Validators
{
    internal class ProductDtoValidation : AbstractValidator<ProductDto>
    {
        public ProductDtoValidation()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().MaximumLength(250);
            RuleFor(p => p.Sku).NotEmpty().NotNull().MaximumLength(250)
                .Must(v => CheckIfExisted(v) == false).WithMessage("Sku must be unique");
            RuleFor(p => p.BrandId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.CategoryId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.SupplierId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.SellingPrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.PurchasePrice).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(p => p.QuantityPerUnit).NotEmpty().NotNull();
            RuleFor(p => p.Type).NotEmpty().NotNull();
        }

        private bool CheckIfExisted(string value)
        {
            var instance = GeneralInstancesImplementations<IPublicCatalogApi>.GetInstanceOfService();
            return instance?.NameAlreadyExists<Product>(x => x.Sku == value).Result ?? false;
        }
    }
}

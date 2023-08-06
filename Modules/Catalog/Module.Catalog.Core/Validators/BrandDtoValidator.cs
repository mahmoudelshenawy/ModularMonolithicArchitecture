using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using Module.Catalog.Shared;
using Module.Catalog.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Validators
{
    internal class BrandDtoValidator : AbstractValidator<BrandDto>
    {
        public BrandDtoValidator()
        {

            RuleFor(p => p.Name).NotEmpty().NotNull().MaximumLength(100)
                .Must((x) => CheckIfExisted(x) == false).WithMessage("the name already exists");
            RuleFor(p => p.Description).MaximumLength(250);

        }

        private bool CheckIfExisted(string value)
        {
            var instance = GeneralInstancesImplementations<IPublicCatalogApi>.GetInstanceOfService();
            return instance?.NameAlreadyExists<Brand>(x => x.Name == value).Result ?? false;
        }

    }
}  

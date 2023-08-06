using FluentValidation;
using Module.Catalog.Core.Dtos;
using Module.Catalog.Core.Entities;
using Module.Catalog.Shared.Interfaces;
using Module.Catalog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Validators
{
    internal class CategoryDtoValidation : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidation()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().MaximumLength(100)
               .Must((x) => CheckIfExisted(x) == false).WithMessage("the name already exists");
            RuleFor(p => p.Description).MaximumLength(250);

        }
        private bool CheckIfExisted(string value)
        {
            var instance = GeneralInstancesImplementations<IPublicCatalogApi>.GetInstanceOfService();
            return instance?.NameAlreadyExists<Category>(x => x.Name == value).Result ?? false;
        }
    }
}

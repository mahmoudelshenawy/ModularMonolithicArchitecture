using FluentValidation;
using Module.Catalog.Core.Dtos;

namespace Module.Catalog.Core.Validators
{
    public class SupplierDtoValidator : AbstractValidator<SupplierDto>
    {
        public SupplierDtoValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.ContactName).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.Address).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.Phone).NotEmpty().NotNull().MaximumLength(20);
        }
    }
}

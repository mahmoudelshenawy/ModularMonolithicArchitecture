using FluentValidation;
using Module.Employees.Core.Dtos;

namespace Module.Employees.Core.Validators
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            //RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            //RuleFor(x => x.BranchId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}

using FluentValidation;
using Module.Employees.Core.Dtos;

namespace Module.Employees.Core.Commands.Departments.CreateDepartment
{
    internal class CreateDepartmentAsyncCommandValidator : AbstractValidator<CreateDepartmentAsyncCommand>
    {
        public CreateDepartmentAsyncCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.BranchId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}

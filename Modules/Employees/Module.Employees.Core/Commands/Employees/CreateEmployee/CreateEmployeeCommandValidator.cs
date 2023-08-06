using FluentValidation;
using Module.Employees.Core.Dtos;

namespace Module.Employees.Core.Commands.Employees.CreateEmployee
{
    internal class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeDto).NotNull().SetValidator(new EmployeeCreateDtoValidator());
        }

        private class EmployeeCreateDtoValidator : AbstractValidator<CreateEmployeeDto>
        {
            public EmployeeCreateDtoValidator()
            {
                RuleFor(p => p.BaseSalary).NotEmpty().NotEmpty().GreaterThan(100);
                RuleFor(p => p.BranchId).NotEmpty().NotEmpty().GreaterThan(0);
                RuleFor(p => p.DepartmentId).NotEmpty().NotEmpty().GreaterThan(0);
                RuleFor(p => p.Address).NotEmpty().NotEmpty();
                RuleFor(p => p.Phone).NotEmpty().NotEmpty();
                RuleFor(p => p.Age).NotEmpty().NotEmpty().GreaterThan(18).LessThan(70);
                RuleFor(p => p.Email).NotEmpty().NotEmpty().EmailAddress();
                RuleFor(p => p.Password).NotEmpty().NotEmpty().MinimumLength(6).MaximumLength(20);
                RuleFor(p => p.Status).NotEmpty().NotEmpty();
                RuleFor(p => p.WorkHours).NotEmpty().NotEmpty().GreaterThanOrEqualTo(6);
                RuleFor(p => p.Name).NotEmpty().NotEmpty().MinimumLength(5).MaximumLength(100);
            }
        }
    }


}

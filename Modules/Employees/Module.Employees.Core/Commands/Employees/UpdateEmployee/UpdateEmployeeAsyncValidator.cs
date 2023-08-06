using FluentValidation;
using Module.Employees.Core.Dtos;

namespace Module.Employees.Core.Commands.Employees.UpdateEmployee;

public class UpdateEmployeeAsyncValidator : AbstractValidator<UpdateEmployeeAsyncCommand>
{
    public UpdateEmployeeAsyncValidator()
    {
        RuleFor(x => x.EmployeeDto).NotNull().SetValidator(new EmployeeUpdateDtoValidator());
    }

    private class EmployeeUpdateDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
            RuleFor(p => p.BaseSalary).GreaterThan(100).When(x => x.BaseSalary > 0);
            RuleFor(p => p.BranchId).GreaterThan(0).When(x => x.BranchId != 0);
            RuleFor(p => p.DepartmentId).GreaterThan(0).When(x => x.DepartmentId != 0);
            RuleFor(p => p.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(p => p.Password).MinimumLength(6)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Password));
            RuleFor(p => p.WorkHours).GreaterThanOrEqualTo(6).When(x => x.WorkHours > 0);
            RuleFor(p => p.Name).MinimumLength(5).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Name));
        }
    }

}


using FluentValidation;
using Module.Employees.Core.Entities;
using Module.Employees.Shared.Interfaces;
using Module.Employees.Shared.Services;

namespace Module.Employees.Core.Commands.Branches.CreateBranch
{
    internal class CreateBranchAsyncCommandValidator : AbstractValidator<CreateBranchAsyncCommand>
    {
        public CreateBranchAsyncCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Must(v => NameIsUnique(v) == false).WithMessage("Name is already taken");
        }

        private bool NameIsUnique(string name)
        {
            var instance = GeneralInstancesImplementations<IPublicEmployeeApi>.GetInstanceOfService();
           return instance?.NameAlreadyExists<Branch>(x => x.Name == name).Result ?? false;
        }
    }
}

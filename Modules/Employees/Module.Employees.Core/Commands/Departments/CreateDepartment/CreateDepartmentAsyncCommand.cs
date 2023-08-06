using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Module.Employees.Core.Events;
using Shared.Core.Common.Messaging;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Departments.CreateDepartment
{
    public sealed record CreateDepartmentAsyncCommand(string Name, int BranchId) : ICommand<DepartmentDto>;



    internal sealed class CreateDepartmentAsyncCommandHandler : ICommandHandler<CreateDepartmentAsyncCommand, DepartmentDto>
    {
        private readonly IEmployeeDbContext _context;

        public CreateDepartmentAsyncCommandHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DepartmentDto>> Handle(CreateDepartmentAsyncCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Name) || request.BranchId == 0)
            {
               return Result.Failure<DepartmentDto>(new Error("Input.Invalid" , "Please provide valid inputs"));
            }
            var department = Department.Create(request.Name, request.BranchId);
            await _context.Departments.AddAsync(department);
            department.AddBackgroundDomainEvent(new NewDepartmentCreatedEvent(null, department.Id));
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(DepartmentDto.Create(department.Id, request.Name, request.BranchId));
        }

    }


}

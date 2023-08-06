using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Departments.UpdateDepartment
{
    public sealed record UpdateDepartmentAsyncCommand(DepartmentDto departmentDto) : ICommand<DepartmentDto>;

    internal sealed class UpdateDepartmentAsyncCommandHandler : ICommandHandler<UpdateDepartmentAsyncCommand, DepartmentDto>
    {
        private readonly IEmployeeDbContext _context;

        public UpdateDepartmentAsyncCommandHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<DepartmentDto>> Handle(UpdateDepartmentAsyncCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.departmentDto.Id);
            if (department is null) throw new EntityNotFound("Department of id" + request.departmentDto.Id);

            department = Department.Update(department, request.departmentDto.Name, request.departmentDto.BranchId);

            _context.Departments.Update(department);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(request.departmentDto);
        }
    }


}

using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Departments.DeleteDepartment
{
    public sealed record DeleteDepartmentAsyncCommand(int Id) : ICommand<bool>;

    internal sealed class DeleteDepartmentAsyncCommandHandler : ICommandHandler<DeleteDepartmentAsyncCommand, bool>
    {
        private readonly IEmployeeDbContext _context;

        public DeleteDepartmentAsyncCommandHandler(IEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteDepartmentAsyncCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.Id);
            if (department is null) throw new EntityNotFound("Department of id" + request.Id);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success<DepartmentDto>();
        }
    }
}

using AutoMapper;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Queries.Departments.GetByIdAsync
{
    public sealed record GetDepartmentByIdAsyncQuery(int Id) : IQuery<DepartmentDto>;


    internal sealed class GetDepartmentByIdAsyncQueryHandler : IQueryHandler<GetDepartmentByIdAsyncQuery, DepartmentDto>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetDepartmentByIdAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments.FindAsync(request.Id);
            if (department is null) throw new EntityNotFound("Department of id" + request.Id);

            var dto = DepartmentDto.Create(department.Id, department.Name, department.BranchId);
            return Result.Success<DepartmentDto>(dto);
        }
    }
}

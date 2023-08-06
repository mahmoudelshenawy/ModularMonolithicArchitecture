using AutoMapper;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Queries.Employees.GetByIdAsync
{
    public record GetEmployeeByIdAsyncQuery(Guid Id) : IQuery<EmployeeDto>;

    internal sealed class GetEmployeeByIdAsyncQueryHandler : IQueryHandler<GetEmployeeByIdAsyncQuery, EmployeeDto>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            if(employee is null)
            {
                throw new EntityNotFound("Employee of id" + request.Id);
            }

            var dto = _mapper.Map<EmployeeDto>(employee);
            return Result.Success(dto);
        }
    }
}

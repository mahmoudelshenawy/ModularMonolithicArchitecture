using AutoMapper;
using AutoMapper.QueryableExtensions;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Infrastructure.Common;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Employees.Core.Queries.Employees.GetAllAsync
{
    public record GetAllEmployeesAsyncQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    internal sealed class GetAllEmployeesAsyncQueryHandler : IQueryHandler<GetAllEmployeesAsyncQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetAllEmployeesAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<EmployeeDto>>> Handle(GetAllEmployeesAsyncQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<EmployeeDto> employees = await _context.Employees.OrderByDescending(x => x.CreatedAt)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber , request.PageSize);

            return Result.Success(employees);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Shared.Core.Common.Messaging;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Employees.Core.Queries.Departments.GetAllAsync
{
    public sealed record GetAllDepartmentsAsyncQuery : IQuery<List<DepartmentDto>>;

    internal sealed class GetAllDepartmentsAsyncQueryHandler : IQueryHandler<GetAllDepartmentsAsyncQuery, List<DepartmentDto>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;
        public GetAllDepartmentsAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<DepartmentDto>>> Handle(GetAllDepartmentsAsyncQuery request, CancellationToken cancellationToken)
        {
            var departments = await _context.Departments.ToListAsync(cancellationToken);
            var dtoList = _mapper.Map<List<DepartmentDto>>(departments);
            return Result.Success<List<DepartmentDto>>(dtoList);
        }
    }


}

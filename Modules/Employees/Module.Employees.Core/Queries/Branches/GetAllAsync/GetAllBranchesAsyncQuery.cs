using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Infrastructure.Common;
using Shared.Models.Models;

namespace Module.Employees.Core.Queries.Branches.GetAllAsync
{
    public record GetAllBranchesAsyncQuery(int PageNumber, int PageSize) : IQuery<PaginatedList<BranchDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    
    internal sealed class GetAllBranchesAsyncQueryHandler : IQueryHandler<GetAllBranchesAsyncQuery, PaginatedList<BranchDto>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;
        public GetAllBranchesAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<BranchDto>>> Handle(GetAllBranchesAsyncQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<BranchDto> BranchDtoList = await _context.Branches.OrderByDescending(x => x.CreatedAt)
               .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);

            return Result.Success(BranchDtoList);
        }

        //public async Task<PaginatedList<BranchDto>> Handle(GetAllBranchesAsyncQuery request, CancellationToken cancellationToken)
        //{
        //    //Result<T>
        //    var branches = await _context.Branches.ToListAsync();
        //    List<BranchDto> dto =  branches.Select(x =>  BranchDto.Create(x.Id , x.Name)).ToList();

        //    //Paged List
        //    return await _context.Branches.OrderByDescending(x => x.CreatedAt)
        //       .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
        //       .PaginatedListAsync(request.PageNumber, request.PageSize);
        //}


    }


}

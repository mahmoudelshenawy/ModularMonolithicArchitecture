using AutoMapper;
using MediatR;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Shared.Core.Common.Messaging;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Queries.Branches.GetByIdAsync
{
    public record GetBranchByIdAsyncQuery(int Id) : IQuery<BranchDto>;

    internal sealed class GetBranchByIdAsyncQueryHandler : IQueryHandler<GetBranchByIdAsyncQuery, BranchDto>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;

        public GetBranchByIdAsyncQueryHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<BranchDto>> Handle(GetBranchByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FindAsync(new object[] { request.Id });
            if (branch == null) throw new EntityNotFound("Branch");

            var dto = _mapper.Map<BranchDto>(branch);
            return Result.Success(dto);
        }

       
        
    }
}

using AutoMapper;
using MediatR;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Branches.UpdateBranch
{
    public record UpdateBranchAsyncCommand(BranchDto BranchDto) : IRequest<Result<BranchDto>>;


    internal sealed class UpdateBranchAsyncCommandHandler : IRequestHandler<UpdateBranchAsyncCommand, Result<BranchDto>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBranchAsyncCommandHandler(IEmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<BranchDto>> Handle(UpdateBranchAsyncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(new object[] { request.BranchDto.Id });
                if (branch == null) throw new EntityNotFound("Branch");

                branch = Branch.Update(branch, request.BranchDto.Name);
                _context.Branches.Update(branch);
                await _context.SaveChangesAsync(cancellationToken);
                var dto = _mapper.Map<BranchDto>(branch);
                return Result.Success(dto);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

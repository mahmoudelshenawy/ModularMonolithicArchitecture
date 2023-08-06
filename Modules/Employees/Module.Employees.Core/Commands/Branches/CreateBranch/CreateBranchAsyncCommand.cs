using AutoMapper;
using MediatR;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Dtos;
using Module.Employees.Core.Entities;
using Module.Employees.Core.Events;
using Shared.Core.Common.EventBus;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Branches.CreateBranch
{
    public record CreateBranchAsyncCommand(string Name) : IRequest<Result<BranchDto>>;
    
    

    internal sealed class CreateBranchAsyncCommandHandler : IRequestHandler<CreateBranchAsyncCommand, Result<BranchDto>>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CreateBranchAsyncCommandHandler(IEmployeeDbContext context, IMapper mapper, IEventBus eventBus)
        {
            _context = context;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Result<BranchDto>> Handle(CreateBranchAsyncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = Branch.Create(request.Name);
                await _context.Branches.AddAsync(branch);
                var branchDto = _mapper.Map<BranchDto>(branch);
               // branch.AddBackgroundDomainEvent(new NewBranchIsCreatedEvent(branchDto));
                await _context.SaveChangesAsync(cancellationToken);
                await _eventBus.PublishAsync(new BranchCreatedAsyncEvent() { 
                    Id = branch.Id , 
                    Name = branch.Name,
                    Message = @$"the branch is created at {branch.CreatedAt} by  {branch.CreatedBy}"
                }, cancellationToken);
                return Result.Success(branchDto);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }


}

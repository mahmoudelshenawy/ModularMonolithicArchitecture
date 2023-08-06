using MediatR;
using Module.Employees.Core.Abstractions;
using Module.Employees.Core.Commands.Branches.DeleteBranch.Saga;
using Module.Employees.Core.Events;
using Shared.Core.Common.EventBus;
using Shared.Core.Exceptions;
using Shared.Models.Models;

namespace Module.Employees.Core.Commands.Branches.DeleteBranch
{
    public record DeleteBranchAsyncCommand(int Id) : IRequest<Result>;

    internal sealed class DeleteBranchAsyncCommandHandler : IRequestHandler<DeleteBranchAsyncCommand, Result>
    {
        private readonly IEmployeeDbContext _context;
        private readonly IEventBus _eventBus;
        public DeleteBranchAsyncCommandHandler(IEmployeeDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(DeleteBranchAsyncCommand request, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FindAsync(new object[] { request.Id });
            if (branch == null) throw new EntityNotFound("Branch");

            _context.Branches.Remove(branch);

            branch.AddDomainEvent(new BranchIsDeletedEvent(null,branch.Id));

            await _context.SaveChangesAsync(cancellationToken);
            await _eventBus.PublishAsync(new BranchDeletedAsyncEvent
            {
                Id = request.Id,
                Message = $"{branch.ModifiedBy} --- {branch.UpdatedAt}"
            });
            return Result.Success();
        }
    }


}

using MediatR;
using Module.Catalog.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Module.Catalog.Core.Commands.Suppliers.DeleteSupplier
{
    public record DeleteProductAsyncCommand(int Id) : IRequest<bool>;

    internal class DeleteSupplierAsyncCommandHandler : IRequestHandler<DeleteProductAsyncCommand, bool>
    {
        private readonly ICatalogDbContext _context;

        public DeleteSupplierAsyncCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(new object[] { request.Id });
            if (supplier == null)
                throw new EntityNotFound("supplier");

         return  await _context.SaveChangesAsync(cancellationToken) > 0 ? true : false;
        }
    }
}

using MediatR;
using Module.Catalog.Core.Abstractions;
using Shared.Core.Exceptions;

namespace Module.Catalog.Core.Commands.Products.DeleteProduct
{
    public record DeleteProductAsyncCommand(int Id) : IRequest<bool>;

    internal class DeleteProductAsyncCommandHandler : IRequestHandler<DeleteProductAsyncCommand, bool>
    {
        private readonly ICatalogDbContext _context;

        public DeleteProductAsyncCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductAsyncCommand request, CancellationToken cancellationToken)
        {
            var Product = await _context.Products.FindAsync(new object[] { request.Id });
            if (Product == null)
                throw new EntityNotFound("Product");

         return  await _context.SaveChangesAsync(cancellationToken) > 0 ? true : false;
        }
    }
}

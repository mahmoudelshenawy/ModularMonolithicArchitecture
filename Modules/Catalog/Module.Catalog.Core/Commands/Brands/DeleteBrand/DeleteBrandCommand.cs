using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;

namespace Module.Catalog.Core.Commands.Brands.DeleteBrand
{
    public record DeleteBrandCommand(int Id) : IRequest<bool>;


    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly ICatalogDbContext _context;

        public DeleteBrandCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == request.Id);
            _context.Brands.Remove(brand);
            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}

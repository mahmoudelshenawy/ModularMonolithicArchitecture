using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Core.Commands.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(int id) : IRequest<bool>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICatalogDbContext _context;

        public DeleteCategoryCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.id);
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync(cancellationToken) > 0;

        }
    }


}

using Module.Catalog.Infrastructure.Persistence;
using Module.Catalog.Shared.Interfaces;
using System.Linq.Expressions;

namespace Module.Catalog.Infrastructure.Services
{
    public class PublicCatalogApiRepository : IPublicCatalogApi
    {
        private readonly CatalogDbContext _context;

        public PublicCatalogApiRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public Task<bool> NameAlreadyExists<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Task.FromResult(_context.Set<T>().Any(expression));
        }
    }
}

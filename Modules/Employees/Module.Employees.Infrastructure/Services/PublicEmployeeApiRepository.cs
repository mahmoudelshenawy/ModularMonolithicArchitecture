using Microsoft.EntityFrameworkCore;
using Module.Employees.Infrastructure.Persistence;
using Module.Employees.Shared.Interfaces;
using System.Linq.Expressions;

namespace Module.Employees.Infrastructure.Services
{
    public class PublicEmployeeApiRepository : IPublicEmployeeApi
    {
        private readonly EmployeeDbContext _context;

        public PublicEmployeeApiRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> NameAlreadyExists<T>(Expression<Func<T, bool>> expression) where T : class
        {
          return await  _context.Set<T>().AnyAsync(expression);
        }
    }
}

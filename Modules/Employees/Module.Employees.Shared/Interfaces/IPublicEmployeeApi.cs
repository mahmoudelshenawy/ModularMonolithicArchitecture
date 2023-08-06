using System.Linq.Expressions;

namespace Module.Employees.Shared.Interfaces
{
    public interface IPublicEmployeeApi
    {
        Task<bool> NameAlreadyExists<T>(Expression<Func<T, bool>> expression) where T : class;
    }
}

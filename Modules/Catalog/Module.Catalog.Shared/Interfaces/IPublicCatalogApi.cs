using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Shared.Interfaces
{
    public interface IPublicCatalogApi
    {
        Task<bool> NameAlreadyExists<T>( Expression<Func<T, bool>> expression) where T :class;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Core.Extensions;
using Module.Catalog.Infrastructure.Extensions;
namespace Module.Catalog.Extensions
{
    public static class ModuleExtensionsCollector
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCatalogCore()
                .AddCatalogInfrastructure(configuration);
            return services;
        }
    }
}

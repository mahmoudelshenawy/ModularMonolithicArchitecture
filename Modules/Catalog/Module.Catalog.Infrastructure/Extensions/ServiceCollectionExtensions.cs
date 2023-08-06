using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Core.Abstractions;
using Module.Catalog.Infrastructure.Persistence;
using Module.Catalog.Infrastructure.Services;
using Module.Catalog.Shared;
using Module.Catalog.Shared.Interfaces;
using Shared.Infrastructure.Extensions;

namespace Module.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<CatalogDbContext>(config)
                .AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>())
                .AddScoped<IPublicCatalogApi, PublicCatalogApiRepository>();

            GeneralServiceProvider.ServiceProviders.Add(services.BuildServiceProvider());
            // var scope = services.BuildServiceProvider().CreateScope();
            //if (scope.ServiceProvider.GetRequiredService<IPublicCatalogApi> != null)
            //    GeneralInstancesImplementations<IPublicCatalogApi>.InstanceOfService = scope.ServiceProvider.GetRequiredService<IPublicCatalogApi>();
            return services;
        }
    }
}

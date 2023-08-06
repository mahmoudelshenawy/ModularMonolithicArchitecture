using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Employees.Core.Abstractions;
using Module.Employees.Infrastructure.Persistence;
using Module.Employees.Infrastructure.Services;
using Module.Employees.Shared.Interfaces;
using Module.Employees.Shared.Services;
using Shared.Infrastructure.Extensions;

namespace Module.Employees.Infrastructure.Extensions
{
    public static class ModuleCollectionExtensions
    {
        public static IServiceCollection AddEmployeeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabaseContext<EmployeeDbContext>(configuration)
                .AddScoped<IEmployeeDbContext>(provider => provider.GetService<EmployeeDbContext>())
                 .AddScoped<IPublicEmployeeApi, PublicEmployeeApiRepository>();

            GeneralServiceProvider.ServiceProviders.Add(services.BuildServiceProvider());
            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Employees.Core.Extensions;
using Module.Employees.Infrastructure.Extensions;

namespace Module.Employees.Extensions
{
    public static class ModuleCollectionExtensions
    {
        public static IServiceCollection AddEmployeeModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmployeeCore(configuration)
              .AddEmployeeInfrastructure(configuration);
            return services;
        }
    }
}

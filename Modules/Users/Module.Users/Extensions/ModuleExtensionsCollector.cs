using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Users.Core.Extensions;
using Module.Users.Infrastructure.Extensions;

namespace Module.Users.Extensions
{
    public static class ModuleExtensionsCollector
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserCore(configuration)
                .AddUserInfrastructure(configuration);
            return services;
        }
    }
}

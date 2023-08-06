using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;
using System.Reflection;

namespace Shared.Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection InstallServices(this IServiceCollection services , IConfiguration configuration ,
            params Assembly[] assemblies)
        {
            IEnumerable<IServiceInstaller> serviceInstances = assemblies
                .SelectMany(a => a.DefinedTypes)
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsAssignableTo(typeof(IServiceInstaller)))
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>();

            foreach (IServiceInstaller serviceInstaller in serviceInstances)
            {
                serviceInstaller.Install(services, configuration);
            }
            return services;
        }


    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Module.Employees.Shared.Services
{
    public static class GeneralInstancesImplementations<T> where T : class
    {
        public static T? InstanceOfService;
        public static List<ServiceProvider> _services = GeneralServiceProvider.ServiceProviders;

        public static T? GetInstanceOfService()
        {
            if (_services.Count() > 0)
            {
                foreach (var service in _services)
                {
                    if (service != null && service.GetService<T>() != null)
                    {
                        GeneralInstancesImplementations<T>.InstanceOfService = service.GetRequiredService<T>();
                        return InstanceOfService;
                    }
                }
            }
            return null;
        }

        public static void Clear()
        {
            InstanceOfService = null;
        }
    }
}

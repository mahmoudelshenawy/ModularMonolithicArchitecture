using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Shared.Interfaces;

namespace Module.Catalog.Shared
{
    public static class GeneralInstancesImplementations<T> where T : class
    {
        public static T? InstanceOfService;

        //public static List<T> Instaces = new();

        public static ServiceProvider _service = GeneralServiceProvider.ServiceProvider;

        public static List<ServiceProvider> _services = GeneralServiceProvider.ServiceProviders;

        public static T? GetInstanceOfService()
        {
             //OptionA ==> Use single sevice provider ==> but it is not general to the whole module

            //if (_service != null && _service.GetRequiredService<T>() != null)
            //    GeneralInstancesImplementations<T>.InstanceOfService = _service.GetRequiredService<T>();
            //return InstanceOfService;

            //OptionB ==> Add All ServiceProviders to list ==> more flexible
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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Shared
{
    public static class GeneralServiceProvider
    {
        public static ServiceProvider ServiceProvider;
        public static List<ServiceProvider> ServiceProviders = new();
    }
}

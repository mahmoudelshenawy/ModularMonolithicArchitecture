using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Core.Commands.Brands.CreateBrand;
using Module.Catalog.Core.Validators;
using Module.Catalog.Shared;
using Module.Catalog.Shared.Interfaces;
using Shared.Core.StaticHolders;
using Shared.Models.Models;
using System.Reflection;

namespace Module.Catalog.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogCore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<BrandDtoValidator>(); // register validators
            services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
            services.AddFluentValidationClientsideAdapters(); // for client side

            MassTransitConfiguratorBus.MassTransitConfigurator.AddConsumer<BrandCreatedAsyncEventConsumer>();
            GeneralServiceProvider.ServiceProviders.Add(services.BuildServiceProvider());
            return services;
        }
    }
}

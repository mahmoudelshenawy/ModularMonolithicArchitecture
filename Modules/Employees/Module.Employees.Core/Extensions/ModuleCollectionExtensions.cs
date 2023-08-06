using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Employees.Core.Commands.Branches.CreateBranch;
using Module.Employees.Core.Commands.Branches.DeleteBranch.Saga;
using Module.Employees.Core.Validators;
using Module.Employees.Shared.Services;
using Shared.Core.StaticHolders;
using Shared.Infrastructure.Idempotence;
using Shared.Models.Models;
using System.Reflection;

namespace Module.Employees.Core.Extensions
{
    public static class ModuleCollectionExtensions
    {
        public static IServiceCollection AddEmployeeCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<BranchDtoValidator>(); // register validators
            services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
            services.AddFluentValidationClientsideAdapters(); // for client side
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
            services.AddScoped<BranchCreatedAsyncEventConsumer>();
           // services.AddScoped<BranchDeletedAsyncEventConsumer>();

            //Register consumers
            MassTransitConfiguratorBus.MassTransitConfigurator.AddConsumer<BranchCreatedAsyncEventConsumer>();
            MassTransitConfiguratorBus.MassTransitConfigurator.AddConsumer<BranchDeletedAsyncEventConsumer>();

            GeneralServiceProvider.ServiceProviders.Add(services.BuildServiceProvider());

            return services;
        }
    }
}

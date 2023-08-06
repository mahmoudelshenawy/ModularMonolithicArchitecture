using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Shared.Core.Common.EventBus;
using Shared.Core.Interfaces;
using Shared.Core.StaticHolders;
using Shared.Infrastructure.BackgroundJobs;
using Shared.Infrastructure.Behaviors;
using Shared.Infrastructure.Controllers;
using Shared.Infrastructure.Idempotence;
using Shared.Infrastructure.Interceptors;
using Shared.Infrastructure.MessageBroker;
using Shared.Infrastructure.Persistence;
using Shared.Infrastructure.Services;
using Shared.Models.DockerEnviroments;
using Shared.Models.Models;
using System.Reflection;
using System.Reflection.Metadata;

namespace Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
            services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
           
            services.AddTransient<IEventBus, EventBus>();
            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                // busConfigurator.AddConsumer<BranchCreatedAsyncEventConsumer>();
                // services.AddGenericMassTransitConsumers(busConfigurator);
                MassTransitConfiguratorBus.MassTransitConfigurator = busConfigurator;
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                              trigger.ForJob(jobKey)
                              .WithSimpleSchedule(schedule =>
                              schedule.WithIntervalInMinutes(2).RepeatForever())
                );

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService();
            services.AddScoped<BackgroundDomainEventSaveChangesInterceptor>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IGeneralDbContext>(provider => provider.GetService<GeneralDbContext>());
            services.AddDatabaseContext<GeneralDbContext>(configuration);
            return services;
        }

        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            /* Database Context Dependency Injection */
            var envirmonmentVariables = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("{dbHost}" ,DockerEnvironments.SqlServerDB_HOST),
                new KeyValuePair<string, string>("{dbName}" , DockerEnvironments.SqlServerDB_NAME),
                new KeyValuePair<string, string>("{dbPassword}" , DockerEnvironments.SqlServerDB_SA_PASSWORD)
            };
            string defaultConnectionString = configuration.GetConnectionString("DefaultConnection")!;
            string dockerConnectionString = configuration.GetConnectionString("DockerComposeConnection")!;
            var databseConfig = new DatabaseConfig(configuration.GetValue<bool>("IsContainerized"),
                defaultConnectionString, dockerConnectionString, envirmonmentVariables);
            services.AddMSSQL<T>(databseConfig.ConnectionString);
            return services;
        }
        public static IServiceCollection AddMSSQL<T>(this IServiceCollection services, string connectionString)
            where T : DbContext
        {
            services.AddDbContext<T>(options =>
            options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            return services;
        }

        private static IServiceCollection AddGenericMassTransitConsumers(this IServiceCollection services,
            IBusRegistrationConfigurator configurator)
        {
            List<Assembly> assemblies = new List<Assembly>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }
            IEnumerable<IEventConsumer> serviceInstances = assemblies.SelectMany(a => a.DefinedTypes)
                .Where(a => !a.IsAbstract && !a.IsInterface && a.IsAssignableFrom(typeof(IEventConsumer)))
                .Select(Activator.CreateInstance).Cast<IEventConsumer>();

            foreach (var instance in serviceInstances)
            {
                configurator.AddConsumer(instance.GetType());
            }
            return services;
        }
    }
}

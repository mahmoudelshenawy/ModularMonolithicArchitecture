using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Models;
using System.Reflection;

namespace Module.Users.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserCore(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtConfig>(config.GetSection("JWT"));
            services.Configure<SMTPConfig>(config.GetSection("SMTPConfig"));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           // services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BrandDtoValidator>());
            return services;
        }
    }
}

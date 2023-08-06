using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Module.Users.Core.Abstractions;
using Module.Users.Core.Entities;
using Module.Users.Core.Interfaces;
using Module.Users.Infrastructure.Persistence;
using Module.Users.Infrastructure.Persistence.Seeders;
using Module.Users.Infrastructure.Services;
using Module.Users.Shared.UserApiInterfaces;
using Shared.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<UserDbContext>()
              .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            });
            services
                .AddDatabaseContext<UserDbContext>(config)
                .AddScoped<IUserDbContext>(provider => provider.GetService<UserDbContext>())
                .AddScoped<UserDbContextInitializer>()
                .AddScoped<IJwtAuthenticationManager, JwtAuthenticationManager>()
                .AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>()
                .AddScoped<IAdminAuthenticationRepository, AdminAuthenticationRepository>()
                .AddScoped<IUserPublicApi, PublicUserApiService>();
            return services;
        }
    }
}

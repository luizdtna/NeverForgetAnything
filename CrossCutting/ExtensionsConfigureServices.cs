using Application.Application;
using Application.Interfaces;
using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CrossCutting
{
    public static class ExtensionsConfigureServices
    {
        public static void SetInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<IItemApplication, ItemApplication>();
            services.AddScoped<IItemRepository, ItemRepository>();
        }

        public static void SetDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SqlConext>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("WebApiDatabase");
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
        }

        public static void SetAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Util.ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    ValidAudience = Util.ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Util.ConfigurationManager.AppSetting["JWT:Secret"]))
                };
            });
        }
    }
}
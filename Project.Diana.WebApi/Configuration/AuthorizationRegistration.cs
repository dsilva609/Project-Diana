using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.WebApi.Configuration
{
    public static class AuthorizationRegistration
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ProjectDianaReadonlyContext>()
               .AddEntityFrameworkStores<ProjectDianaWriteContext>()
               .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var issuer = configuration["GlobalSettings:Issuer"];
                    var jwtKey = configuration["GlobalSettings:JwtKey"];

                    options.Audience = issuer;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            return services;
        }
    }
}
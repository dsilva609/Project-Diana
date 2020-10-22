using Microsoft.Extensions.DependencyInjection;

namespace Project.Diana.WebApi.Configuration
{
    public static class CorsRegistration
    {
        public static IServiceCollection RegisterCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .Build());
            });
    }
}
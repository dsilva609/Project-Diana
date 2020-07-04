using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.Data.Features.Settings;

namespace Project.Diana.WebApi.Configuration
{
    public static class SettingsRegistration
    {
        public static IServiceCollection RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("GlobalSettings").Get<GlobalSettings>();
            var validator = new GlobalSettingsValidator();

            validator.ValidateAndThrow(settings);

            return services.AddSingleton<GlobalSettings>(settings);
        }
    }
}
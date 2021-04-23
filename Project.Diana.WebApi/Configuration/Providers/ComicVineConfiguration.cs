using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.ApiClient.Features.ComicVine;
using Project.Diana.Provider.Features.ComicVine;

namespace Project.Diana.WebApi.Configuration.Providers
{
    public static class ComicVineConfiguration
    {
        public static IServiceCollection AddComicVineProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var comicVineSettings = configuration.GetSection("ComicVineSettings").Get<ComicVineApiClientConfiguration>();
            var comicVineSettingsValidator = new ComicVineApiClientConfigurationValidator();

            comicVineSettingsValidator.ValidateAndThrow(comicVineSettings);

            services.AddSingleton<IComicVineApiClientConfiguration>(comicVineSettings);
            services.AddScoped<IComicVineApiClient, ComicVineApiClient>();
            services.AddScoped<IComicVineProvider, ComicVineProvider>();

            return services;
        }
    }
}
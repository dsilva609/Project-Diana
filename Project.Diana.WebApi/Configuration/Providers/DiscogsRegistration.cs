using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.ApiClient.Features.Discogs;
using Project.Diana.Provider.Features.Discogs;

namespace Project.Diana.WebApi.Configuration.Providers
{
    public static class DiscogsRegistration
    {
        public static IServiceCollection AddDiscogsProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDiscogsApiClientConfiguration>(_ => configuration.GetSection("DiscogsSettings").Get<IDiscogsApiClientConfiguration>());
            services.AddScoped<IDiscogsProvider, DiscogsProvider>();
            services.AddScoped<IDiscogsApiClient, DiscogsApiClient>();

            return services;
        }
    }
}
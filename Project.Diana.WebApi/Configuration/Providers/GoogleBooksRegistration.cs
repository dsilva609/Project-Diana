using Google.Apis.Books.v1;
using Google.Apis.Services;
using Microsoft.Extensions.DependencyInjection;
using Project.Diana.ApiClient.Features.GoogleBooks;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.WebApi.Configuration.Providers
{
    public static class GoogleBooksRegistration
    {
        public static IServiceCollection AddGoogleBooksProvider(this IServiceCollection services)
            => services
                .AddScoped<IClientService, BooksService>()
                .AddScoped<VolumesResource>()
                .AddScoped<IGoogleBooksApiClient, GoogleBooksApiClient>()
                .AddScoped<IGoogleBooksProvider, GoogleBooksProvider>();
    }
}
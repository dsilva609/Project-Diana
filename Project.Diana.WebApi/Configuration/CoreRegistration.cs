using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Project.Diana.WebApi.Configuration
{
    public static class CoreRegistration
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services) => services.AddScoped<IRestClient, RestClient>();
    }
}
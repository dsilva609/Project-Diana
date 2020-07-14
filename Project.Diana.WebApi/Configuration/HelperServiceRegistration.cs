using Microsoft.Extensions.DependencyInjection;
using Project.Diana.WebApi.Helpers;

namespace Project.Diana.WebApi.Configuration
{
    public static class HelperServiceRegistration
    {
        public static IServiceCollection RegisterHelperServices(this IServiceCollection services) => services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
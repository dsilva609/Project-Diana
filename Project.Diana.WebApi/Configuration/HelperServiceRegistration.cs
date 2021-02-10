using Microsoft.Extensions.DependencyInjection;
using Project.Diana.WebApi.Helpers.Token;
using Project.Diana.WebApi.Helpers.User;

namespace Project.Diana.WebApi.Configuration
{
    public static class HelperServiceRegistration
    {
        public static IServiceCollection RegisterHelperServices(this IServiceCollection services)
            => services
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<ITokenService, TokenService>();
    }
}
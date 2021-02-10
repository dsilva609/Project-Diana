using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Helpers.Token
{
    public interface ITokenService
    {
        string GenerateAccessToken(ApplicationUser user);

        RefreshTokenRecord GenerateRefreshToken(ApplicationUser user);
    }
}
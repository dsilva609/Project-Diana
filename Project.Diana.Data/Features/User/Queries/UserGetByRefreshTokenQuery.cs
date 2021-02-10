using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.User.Queries
{
    public class UserGetByRefreshTokenQuery : IQuery<Result<ApplicationUser>>
    {
        public string RefreshToken { get; }

        public UserGetByRefreshTokenQuery(string refreshToken)
        {
            Guard.Against.NullOrWhiteSpace(refreshToken, nameof(refreshToken));

            RefreshToken = refreshToken;
        }
    }
}
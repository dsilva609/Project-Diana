using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Features.RefreshTokens.Commands
{
    public class RefreshTokenClearExpiredForUserCommand : ICommand
    {
        public string ActiveTokenForExpiration { get; }
        public string UserId { get; }

        public RefreshTokenClearExpiredForUserCommand(string activeTokenForExpiration, string userId)
        {
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));

            ActiveTokenForExpiration = activeTokenForExpiration;
            UserId = userId;
        }
    }
}
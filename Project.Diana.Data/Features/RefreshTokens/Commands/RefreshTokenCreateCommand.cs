using System;
using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;

namespace Project.Diana.Data.Features.RefreshTokens.Commands
{
    public class RefreshTokenCreateCommand : ICommand
    {
        public DateTimeOffset ExpiresOn { get; }
        public string Token { get; }
        public string UserId { get; }

        public RefreshTokenCreateCommand(DateTimeOffset expiresOn, string token, string userId)
        {
            Guard.Against.Default(expiresOn, nameof(expiresOn));
            Guard.Against.NullOrWhiteSpace(token, nameof(token));
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));

            ExpiresOn = expiresOn;
            Token = token;
            UserId = userId;
        }
    }
}
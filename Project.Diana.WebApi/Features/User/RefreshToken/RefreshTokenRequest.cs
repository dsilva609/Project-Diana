using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Project.Diana.WebApi.Features.User.RefreshToken
{
    public class RefreshTokenRequest : IRequest<IActionResult>
    {
        public string RefreshToken { get; }

        public RefreshTokenRequest(string refreshToken)
        {
            Guard.Against.NullOrWhiteSpace(refreshToken, nameof(refreshToken));

            RefreshToken = refreshToken;
        }
    }
}
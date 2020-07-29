using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Project.Diana.WebApi.Features.User.Login
{
    public class LoginRequest : IRequest<IActionResult>
    {
        public string Password { get; }
        public string Username { get; }

        public LoginRequest(string username, string password)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password));
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            Password = password;
            Username = username;
        }
    }
}
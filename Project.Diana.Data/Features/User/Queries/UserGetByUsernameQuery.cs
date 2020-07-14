using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.User.Queries
{
    public class UserGetByUsernameQuery : IQuery<ApplicationUser>
    {
        public string Username { get; }

        public UserGetByUsernameQuery(string username)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            Username = username;
        }
    }
}
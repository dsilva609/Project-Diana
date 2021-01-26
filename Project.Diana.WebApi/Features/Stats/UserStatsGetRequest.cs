using Ardalis.GuardClauses;
using MediatR;

namespace Project.Diana.WebApi.Features.Stats
{
    public class UserStatsGetRequest : IRequest<StatsResponse>
    {
        public int UserId { get; }

        public UserStatsGetRequest(int userId)
        {
            Guard.Against.Zero(userId, nameof(userId));

            UserId = userId;
        }
    }
}
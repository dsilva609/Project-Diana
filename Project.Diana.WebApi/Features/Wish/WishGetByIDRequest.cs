using Ardalis.GuardClauses;
using MediatR;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetByIDRequest : IRequest<string>
    {
        public int ID { get; }

        public WishGetByIDRequest(int id)
        {
            Guard.Against.Default(id, nameof(id));

            ID = id;
        }
    }
}
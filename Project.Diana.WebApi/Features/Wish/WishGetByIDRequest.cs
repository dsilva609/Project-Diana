using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetByIDRequest : IRequest<WishRecord>
    {
        public int ID { get; }

        public WishGetByIDRequest(int id)
        {
            Guard.Against.Default(id, nameof(id));

            ID = id;
        }
    }
}
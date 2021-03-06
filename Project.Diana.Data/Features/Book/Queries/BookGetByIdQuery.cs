using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Book.Queries
{
    public class BookGetByIdQuery : IQuery<BookRecord>
    {
        public int Id { get; }
        public ApplicationUser User { get; }

        public BookGetByIdQuery(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Id = id;
            User = user;
        }
    }
}
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using MediatR;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Book.BookById
{
    public class BookGetByIdRequest : IRequest<BookRecord>
    {
        public int Id { get; }
        [CanBeNull] public ApplicationUser User { get; }

        public BookGetByIdRequest(int id, ApplicationUser user)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            Id = id;
            User = user;
        }
    }
}
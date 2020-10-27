using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.WebApi.Features.Book.GetGoogleBookById
{
    public class GetGoogleBookByIdRequest : IRequest<BookSearchResponse>
    {
        public string Id { get; }

        public GetGoogleBookByIdRequest(string id)
        {
            Guard.Against.NullOrWhiteSpace(id, nameof(id));

            Id = id;
        }
    }
}
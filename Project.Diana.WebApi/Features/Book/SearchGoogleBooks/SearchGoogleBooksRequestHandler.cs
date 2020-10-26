using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.WebApi.Features.Book.SearchGoogleBooks
{
    public class SearchGoogleBooksRequestHandler : IRequestHandler<SearchGoogleBooksRequest, IEnumerable<BookSearchResponse>>
    {
        private readonly IGoogleBooksProvider _googleBooksProvider;

        public SearchGoogleBooksRequestHandler(IGoogleBooksProvider googleBooksProvider) => _googleBooksProvider = googleBooksProvider;

        public async Task<IEnumerable<BookSearchResponse>> Handle(SearchGoogleBooksRequest request, CancellationToken cancellationToken)
        {
            var result = await _googleBooksProvider.Search(request.Author, request.Title);

            return result.IsSuccess ? result.Value : throw new Exception(result.Error);
        }
    }
}
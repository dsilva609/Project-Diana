using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.WebApi.Features.Book.GetGoogleBookById
{
    public class GetGoogleBookByIdRequestHandler : IRequestHandler<GetGoogleBookByIdRequest, BookSearchResponse>
    {
        private readonly IGoogleBooksProvider _googleBooksProvider;

        public GetGoogleBookByIdRequestHandler(IGoogleBooksProvider googleBooksProvider) => _googleBooksProvider = googleBooksProvider;

        public async Task<BookSearchResponse> Handle(GetGoogleBookByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _googleBooksProvider.GetVolumeById(request.Id);

            return result.IsSuccess ? result.Value : throw new Exception(result.Error);
        }
    }
}
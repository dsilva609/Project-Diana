using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookList
{
    public class BookListGetRequestHandler : IRequestHandler<BookListGetRequest, BookListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public BookListGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<BookListResponse> Handle(BookListGetRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<BookListGetQuery, BookListResponse>(new BookListGetQuery(request.ItemCount, request.Page, request.SearchQuery, request.User));
    }
}
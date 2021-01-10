using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookGetLatestAdded
{
    public class BookGetLatestAddedRequestHandler : IRequestHandler<BookGetLatestAddedRequest, BookListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public BookGetLatestAddedRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<BookListResponse> Handle(BookGetLatestAddedRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<BookGetLatestAddedQuery, BookListResponse>(new BookGetLatestAddedQuery(request.ItemCount));
    }
}
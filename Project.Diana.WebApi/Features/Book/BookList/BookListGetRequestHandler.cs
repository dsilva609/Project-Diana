using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookList
{
    public class BookListGetRequestHandler : IRequestHandler<BookListGetRequest, IEnumerable<BookRecord>>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public BookListGetRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<IEnumerable<BookRecord>> Handle(BookListGetRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<BookListGetQuery, IEnumerable<BookRecord>>(new BookListGetQuery(request.ItemCount, request.User));
    }
}
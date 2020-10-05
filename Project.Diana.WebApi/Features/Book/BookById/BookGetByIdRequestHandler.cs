using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookById
{
    public class BookGetByIdRequestHandler : IRequestHandler<BookGetByIdRequest, BookRecord>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public BookGetByIdRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<BookRecord> Handle(BookGetByIdRequest request, CancellationToken cancellationToken) => await _queryDispatcher.Dispatch<BookGetByIdQuery, BookRecord>(new BookGetByIdQuery(request.Id, request.User));
    }
}
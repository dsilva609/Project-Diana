using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookIncrementReadCount
{
    public class BookIncrementReadCountRequestHandler : IRequestHandler<BookIncrementReadCountRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookIncrementReadCountRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(BookIncrementReadCountRequest request, CancellationToken cancellationToken) => await _commandDispatcher.Dispatch(new BookIncrementReadCountCommand(request.BookId, request.User));
    }
}
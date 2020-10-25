using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookAddToShowcase
{
    public class BookAddToShowcaseRequestHandler : IRequestHandler<BookAddToShowcaseRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookAddToShowcaseRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(BookAddToShowcaseRequest request, CancellationToken cancellationToken) => await _commandDispatcher.Dispatch(new BookAddToShowcaseCommand(request.BookId, request.User));
    }
}
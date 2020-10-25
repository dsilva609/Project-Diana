using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase
{
    public class BookRemoveFromShowcaseRequestHandler : IRequestHandler<BookRemoveFromShowcaseRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookRemoveFromShowcaseRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(BookRemoveFromShowcaseRequest request, CancellationToken cancellationToken) => await _commandDispatcher.Dispatch(new BookRemoveFromShowcaseCommand(request.Id, request.User));
    }
}
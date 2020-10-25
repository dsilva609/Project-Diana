using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookUpdate
{
    public class BookUpdateRequestHandler : IRequestHandler<BookUpdateRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookUpdateRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(BookUpdateRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(
                new BookUpdateCommand(
                    request.Author,
                    request.BookId,
                    request.Category,
                    request.CompletionStatus,
                    request.CountryOfOrigin,
                    request.CountryPurchased,
                    request.DatePurchased,
                    request.Genre,
                    request.ImageUrl,
                    request.ISBN10,
                    request.ISBN13,
                    request.IsFirstEdition,
                    request.IsHardcover,
                    request.IsNewPurchase,
                    request.IsPhysical,
                    request.IsReissue,
                    request.Language,
                    request.LocationPurchased,
                    request.Notes,
                    request.PageCount,
                    request.Publisher,
                    request.TimesCompleted,
                    request.Title,
                    request.Type,
                    request.YearReleased,
                    request.User
                    ));
    }
}
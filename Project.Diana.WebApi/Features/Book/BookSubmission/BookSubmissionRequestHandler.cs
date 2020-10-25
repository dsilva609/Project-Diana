using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Book.BookSubmission
{
    public class BookSubmissionRequestHandler : IRequestHandler<BookSubmissionRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BookSubmissionRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(BookSubmissionRequest request, CancellationToken cancellationToken) =>
            await _commandDispatcher.Dispatch(new BookSubmissionCommand(
                request.Author,
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
                request.User));
    }
}
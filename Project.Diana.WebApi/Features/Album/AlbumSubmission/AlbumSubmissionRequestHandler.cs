using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Album.AlbumSubmission
{
    public class AlbumSubmissionRequestHandler : IRequestHandler<AlbumSubmissionRequest>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AlbumSubmissionRequestHandler(ICommandDispatcher commandDispatcher) => _commandDispatcher = commandDispatcher;

        public async Task<Unit> Handle(AlbumSubmissionRequest request, CancellationToken cancellationToken)
            => await _commandDispatcher.Dispatch(
                new AlbumSubmissionCommand(
                    request.Artist,
                    request.Category,
                    request.CompletionStatus,
                    request.CountryOfOrigin,
                    request.CountryPurchased,
                    request.DatePurchased,
                    request.DiscogsId,
                    request.Genre,
                    request.ImageUrl,
                    request.IsNewPurchase,
                    request.IsPhysical,
                    request.LocationPurchased,
                    request.MediaType,
                    request.Notes,
                    request.RecordLabel,
                    request.Size,
                    request.Speed,
                    request.Style,
                    request.TimesCompleted,
                    request.Title,
                    request.YearReleased,
                    request.User));
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumSubmission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.Submission
{
    public class AlbumSubmissionRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumSubmissionRequestHandler _handler;
        private readonly AlbumSubmissionRequest _testRequest;

        public AlbumSubmissionRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumSubmissionRequest>();

            _handler = new AlbumSubmissionRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumSubmissionCommand>()), Times.Once);
        }
    }
}
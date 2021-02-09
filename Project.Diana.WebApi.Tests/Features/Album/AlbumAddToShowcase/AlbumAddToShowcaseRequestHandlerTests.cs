using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumAddToShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumAddToShowcase
{
    public class AlbumAddToShowcaseRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumAddToShowcaseRequestHandler _handler;
        private readonly AlbumAddToShowcaseRequest _testRequest;

        public AlbumAddToShowcaseRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumAddToShowcaseRequest>();

            _handler = new AlbumAddToShowcaseRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumAddToShowcaseCommand>()), Times.Once);
        }
    }
}
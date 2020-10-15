using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumRemoveFromShowcase
{
    public class AlbumRemoveFromShowcaseRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumRemoveFromShowcaseRequestHandler _handler;
        private readonly AlbumRemoveFromShowcaseRequest _testRequest;

        public AlbumRemoveFromShowcaseRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumRemoveFromShowcaseRequest>();

            _handler = new AlbumRemoveFromShowcaseRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumRemoveFromShowcaseCommand>()), Times.Once);
        }
    }
}
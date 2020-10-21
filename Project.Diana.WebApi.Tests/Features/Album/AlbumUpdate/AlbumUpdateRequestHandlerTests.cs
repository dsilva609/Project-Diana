using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumUpdate;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumUpdate
{
    public class AlbumUpdateRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumUpdateRequestHandler _handler;
        private readonly AlbumUpdateRequest _testRequest;

        public AlbumUpdateRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumUpdateRequest>();

            _handler = new AlbumUpdateRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumUpdateCommand>()), Times.Once);
        }
    }
}
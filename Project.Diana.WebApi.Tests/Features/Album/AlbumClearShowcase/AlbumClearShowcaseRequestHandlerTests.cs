using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumClearShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumClearShowcase
{
    public class AlbumClearShowcaseRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumClearShowcaseRequestHandler _handler;
        private readonly AlbumClearShowcaseRequest _testRequest;

        public AlbumClearShowcaseRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumClearShowcaseRequest>();

            _handler = new AlbumClearShowcaseRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumClearShowcaseCommand>()), Times.Once);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Album.AlbumIncrementPlayCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Album.AlbumIncrementPlayCount
{
    public class AlbumIncrementPlayCountRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly AlbumIncrementPlayCountRequestHandler _handler;
        private readonly AlbumIncrementPlayCountRequest _testRequest;

        public AlbumIncrementPlayCountRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<AlbumIncrementPlayCountRequest>();

            _handler = new AlbumIncrementPlayCountRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<AlbumIncrementPlayCountCommand>()), Times.Once);
        }
    }
}
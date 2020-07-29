using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish.Update;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Update
{
    public class WishUpdateRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly WishUpdateRequestHandler _handler;
        private readonly WishUpdateRequest _testRequest;

        public WishUpdateRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<WishUpdateRequest>();

            _handler = new WishUpdateRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<WishUpdateCommand>()), Times.Once);
        }
    }
}
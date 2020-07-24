using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish
{
    public class WishCompleteItemRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly WishCompleteItemRequestHandler _handler;
        private readonly WishCompleteItemRequest _testRequest;

        public WishCompleteItemRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<WishCompleteItemRequest>();

            _handler = new WishCompleteItemRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<WishCompleteItemCommand>()), Times.Once);
        }
    }
}
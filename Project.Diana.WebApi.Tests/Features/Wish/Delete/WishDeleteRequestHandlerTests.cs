using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish.Delete;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Delete
{
    public class WishDeleteRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly WishDeleteRequestHandler _handler;
        private readonly WishDeleteRequest _testRequest;

        public WishDeleteRequestHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _commandDispatcher = new Mock<ICommandDispatcher>();

            _testRequest = fixture.Create<WishDeleteRequest>();

            _handler = new WishDeleteRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.Is<WishDeleteCommand>(r => r.Id == _testRequest.WishId)), Times.Once);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Wish.Submission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Wish.Submission
{
    public class WishSubmissionRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly WishSubmissionRequestHandler _handler;
        private readonly WishSubmissionRequest _testRequest;

        public WishSubmissionRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<WishSubmissionRequest>();

            _handler = new WishSubmissionRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(c => c.Dispatch(It.IsNotNull<WishCreateCommand>()), Times.Once);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookSubmission;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookSubmission
{
    public class BookSubmissionRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly BookSubmissionRequestHandler _handler;
        private readonly BookSubmissionRequest _testRequest;

        public BookSubmissionRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<BookSubmissionRequest>();

            _handler = new BookSubmissionRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<BookSubmissionCommand>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Calls_Wish_Delete_Command_If_Linked_Wish_Exists()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _testRequest.LinkedWishId.Should().BeGreaterThan(0);
            _commandDispatcher.Verify(x => x.Dispatch(It.Is<WishDeleteCommand>(x => x.Id == _testRequest.LinkedWishId && x.User == _testRequest.User)), Times.Once);
        }
    }
}
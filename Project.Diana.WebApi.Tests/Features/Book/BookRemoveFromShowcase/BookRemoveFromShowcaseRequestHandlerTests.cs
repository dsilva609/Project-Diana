using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookRemoveFromShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookRemoveFromShowcase
{
    public class BookRemoveFromShowcaseRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly BookRemoveFromShowcaseRequestHandler _handler;
        private readonly BookRemoveFromShowcaseRequest _testRequest;

        public BookRemoveFromShowcaseRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<BookRemoveFromShowcaseRequest>();

            _handler = new BookRemoveFromShowcaseRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<BookRemoveFromShowcaseCommand>()), Times.Once);
        }
    }
}
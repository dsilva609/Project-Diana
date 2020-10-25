using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookAddToShowcase;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookAddToShowcase
{
    public class BookAddToShowcaseRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly BookAddToShowcaseRequestHandler _handler;
        private readonly BookAddToShowcaseRequest _testRequest;

        public BookAddToShowcaseRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<BookAddToShowcaseRequest>();

            _handler = new BookAddToShowcaseRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<BookAddToShowcaseCommand>()), Times.Once);
        }
    }
}
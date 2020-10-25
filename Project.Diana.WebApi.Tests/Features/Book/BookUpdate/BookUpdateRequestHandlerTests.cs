using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookUpdate;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookUpdate
{
    public class BookUpdateRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly BookUpdateRequestHandler _handler;
        private readonly BookUpdateRequest _testRequest;

        public BookUpdateRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<BookUpdateRequest>();

            _handler = new BookUpdateRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<BookUpdateCommand>()), Times.Once);
        }
    }
}
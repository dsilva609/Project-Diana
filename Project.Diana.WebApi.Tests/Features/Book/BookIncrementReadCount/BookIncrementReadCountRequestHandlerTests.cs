using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Book.BookIncrementReadCount;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Book.BookIncrementReadCount
{
    public class BookIncrementReadCountRequestHandlerTests
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly BookIncrementReadCountRequestHandler _handler;
        private readonly BookIncrementReadCountRequest _testRequest;

        public BookIncrementReadCountRequestHandlerTests()
        {
            var fixture = new Fixture();

            _commandDispatcher = new Mock<ICommandDispatcher>();
            _testRequest = fixture.Create<BookIncrementReadCountRequest>();

            _handler = new BookIncrementReadCountRequestHandler(_commandDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Command_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _commandDispatcher.Verify(x => x.Dispatch(It.IsNotNull<BookIncrementReadCountCommand>()), Times.Once);
        }
    }
}
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Commands;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Commands
{
    public class WishCompleteItemCommandHandlerTests
    {
        private readonly WishCompleteItemCommandHandler _handler;
        private readonly WishCompleteItemCommand _testCommand;
        private readonly Mock<IProjectDianaWriteContext> _writeContext;

        public WishCompleteItemCommandHandlerTests()
        {
            var fixture = new Fixture();

            _testCommand = fixture.Create<WishCompleteItemCommand>();
            _writeContext = new Mock<IProjectDianaWriteContext>();

            _handler = new WishCompleteItemCommandHandler(_writeContext.Object);
        }

        [Fact]
        public async Task Handler_Updates_Wish()
        {
            await _handler.Handle(_testCommand);

            1.Should().Be(2);
        }
    }
}
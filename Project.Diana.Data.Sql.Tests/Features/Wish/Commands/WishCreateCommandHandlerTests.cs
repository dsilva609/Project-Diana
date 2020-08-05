using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Commands
{
    public class WishCreateCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly WishCreateCommandHandler _handler;
        private readonly WishCreateCommand _testCommand;
        private readonly IProjectDianaWriteContext _writeContext;

        public WishCreateCommandHandlerTests()
        {
            var fixture = new Fixture();

            _testCommand = fixture.Create<WishCreateCommand>();
            _writeContext = InitializeDatabase();

            _handler = new WishCreateCommandHandler(_writeContext);
        }

        [Fact]
        public async Task Handler_Adds_New_Wish()
        {
            await _handler.Handle(_testCommand);

            var wish = await _writeContext.Wishes.FirstOrDefaultAsync(wish => wish.Title == _testCommand.Title);

            wish.Should().NotBeNull();
            wish.UserID.Should().Be(_testCommand.UserID);
        }
    }
}
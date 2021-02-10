using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.RefreshToken.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.RefreshToken.Commands
{
    public class RefreshTokenCreateCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly RefreshTokenCreateCommandHandler _handler;
        private readonly RefreshTokenCreateCommand _testCommand;
        private readonly IProjectDianaWriteContext _writeContext;

        public RefreshTokenCreateCommandHandlerTests()
        {
            var fixture = new Fixture();

            _testCommand = fixture.Create<RefreshTokenCreateCommand>();
            _writeContext = InitializeDatabase();

            _handler = new RefreshTokenCreateCommandHandler(_writeContext);
        }

        [Fact]
        public async Task Handler_Adds_New_Refresh_Token()
        {
            await _handler.Handle(_testCommand);

            var refreshToken = await _writeContext.RefreshTokens.FirstOrDefaultAsync(token => token.Token == _testCommand.Token);

            refreshToken.Should().NotBeNull();
            refreshToken.Id.Should().NotBeEmpty();
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.RefreshToken.Commands;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.RefreshToken.Commands
{
    public class RefreshTokenClearExpiredForUserCommandHandlerTests : DbContextTestBase<ProjectDianaWriteContext>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly IFixture _fixture;
        private readonly RefreshTokenClearExpiredForUserCommandHandler _handler;
        private readonly RefreshTokenClearExpiredForUserCommand _testCommand;

        public RefreshTokenClearExpiredForUserCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();

            _testCommand = _fixture.Create<RefreshTokenClearExpiredForUserCommand>();

            _handler = new RefreshTokenClearExpiredForUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handler_Does_Not_Remove_Matching_Token_For_Other_User()
        {
            var token = new RefreshTokenRecord
            {
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
                Token = _testCommand.ActiveTokenForExpiration,
                UserId = "different user"
            };

            await _context.RefreshTokens.AddAsync(token);

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            (await _context.RefreshTokens.AnyAsync(t => t.Token == _testCommand.ActiveTokenForExpiration)).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Removed_Expired_Tokens()
        {
            await InitializeRecords();

            await _handler.Handle(_testCommand);

            var tokens = await _context.RefreshTokens.Where(token => token.UserId == _testCommand.UserId).ToListAsync();

            tokens.All(token => token.IsActive).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Removes_Active_Token_That_Is_Expired()
        {
            var token = new RefreshTokenRecord
            {
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
                Token = _testCommand.ActiveTokenForExpiration,
                UserId = _testCommand.UserId
            };

            await _context.RefreshTokens.AddAsync(token);

            await InitializeRecords();

            await _handler.Handle(_testCommand);

            (await _context.RefreshTokens.AnyAsync(t => t.Token == _testCommand.ActiveTokenForExpiration)).Should().BeFalse();
        }

        private async Task InitializeRecords()
        {
            var expiredToken = new RefreshTokenRecord
            {
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(-1),
                UserId = _testCommand.UserId
            };

            await _context.RefreshTokens.AddAsync(expiredToken);

            await _context.SaveChangesAsync();
        }
    }
}
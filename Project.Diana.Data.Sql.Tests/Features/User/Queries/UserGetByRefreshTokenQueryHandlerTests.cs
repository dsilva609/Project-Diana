using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.User.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.User.Queries
{
    public class UserGetByRefreshTokenQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly UserGetByRefreshTokenQueryHandler _handler;
        private readonly UserGetByRefreshTokenQuery _testQuery;

        public UserGetByRefreshTokenQueryHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();
            _testQuery = _fixture.Create<UserGetByRefreshTokenQuery>();

            _handler = new UserGetByRefreshTokenQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Failure_When_User_Not_Found()
        {
            await InitializeRecords();

            var query = new UserGetByRefreshTokenQuery("not found token");

            var result = await _handler.Handle(query);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_User()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        private async Task InitializeRecords()
        {
            var refreshToken = _fixture
                .Build<RefreshTokenRecord>()
                .With(t => t.ExpiresOn, DateTimeOffset.UtcNow.AddDays(1))
                .With(t => t.Token, _testQuery.RefreshToken)
                .Create();

            var user = _fixture
                .Build<ApplicationUser>()
                .With(u => u.RefreshTokens, new[] { refreshToken })
                .Create();

            await _context.UserRecords.AddAsync(user);

            await _context.SaveChangesAsync();
        }
    }
}
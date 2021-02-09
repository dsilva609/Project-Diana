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
    public class UserGetByUsernameQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly UserGetByUsernameQueryHandler _handler;
        private readonly UserGetByUsernameQuery _testQuery;
        private readonly ApplicationUser _testUser;
        private readonly Guid testUserId;

        public UserGetByUsernameQueryHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _context = InitializeDatabase();
            _testQuery = _fixture.Create<UserGetByUsernameQuery>();

            testUserId = Guid.NewGuid();

            _testUser = _fixture
                .Build<ApplicationUser>()
                .With(x => x.Id, testUserId.ToString())
                .With(x => x.UserName, _testQuery.Username)
                .Without(x => x.RefreshTokens)
                .Create();

            _handler = new UserGetByUsernameQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_User()
        {
            await InitializeRecords();

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_Returns_User_With_Tokens()
        {
            var tokens = _fixture
                .Build<RefreshTokenRecord>()
                .With(x => x.Id, Guid.NewGuid())
                .With(x => x.UserId, testUserId.ToString)
                .Create();

            _testUser.RefreshTokens = new[] { tokens };

            // await _context.RefreshTokenRecords.AddAsync(tokens);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.RefreshTokens.Should().NotBeEmpty();
        }

        private async Task InitializeRecords()
        {
            await _context.UserRecords.AddAsync(_testUser);

            await _context.SaveChangesAsync();
        }
    }
}
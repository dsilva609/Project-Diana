using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly WishGetByIDQueryHandler _handler;
        private readonly ProjectDianaReadonlyContext _projectDianaContext;
        private readonly WishGetByIDQuery _testQuery;
        private readonly ApplicationUser _testUser;
        private readonly WishRecord _testWishRecord;

        public WishGetByIDQueryHandlerTests()
        {
            var fixture = new Fixture();

            _projectDianaContext = InitializeDatabase();
            _testUser = fixture.Create<ApplicationUser>();

            _testWishRecord = fixture.Create<WishRecord>();
            _testWishRecord.UserID = _testUser.Id;

            _testQuery = new WishGetByIDQuery(_testUser.Id, _testWishRecord.ID);

            InitializeDatabase();

            _handler = new WishGetByIDQueryHandler(_projectDianaContext);
        }

        [Fact]
        public async Task Handler_Retrieves_Wish()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_Returns_Wish_With_Matching_UserID()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.UserID.Should().Be(_testQuery.UserID);
        }

        private async Task InitializeRecords()
        {
            await _projectDianaContext.WishRecords.AddAsync(_testWishRecord);
            await _projectDianaContext.SaveChangesAsync();
        }
    }
}
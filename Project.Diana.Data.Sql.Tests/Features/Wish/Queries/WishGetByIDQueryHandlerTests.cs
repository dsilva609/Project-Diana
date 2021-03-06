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
    public class WishGetByIdQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly WishGetByIdQueryHandler _handler;
        private readonly ProjectDianaReadonlyContext _projectDianaContext;
        private readonly WishGetByIdQuery _testQuery;
        private readonly ApplicationUser _testUser;
        private readonly WishRecord _testWishRecord;

        public WishGetByIdQueryHandlerTests()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _projectDianaContext = InitializeDatabase();
            _testUser = fixture.Create<ApplicationUser>();

            _testWishRecord = fixture.Create<WishRecord>();
            _testWishRecord.UserId = _testUser.Id;

            _testQuery = new WishGetByIdQuery(_testUser.Id, _testWishRecord.Id);

            InitializeDatabase();

            _handler = new WishGetByIdQueryHandler(_projectDianaContext);
        }

        [Fact]
        public async Task Handler_Retrieves_Wish()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_Returns_Wish_With_Matching_UserId()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.UserId.Should().Be(_testQuery.UserId);
        }

        private async Task InitializeRecords()
        {
            await _projectDianaContext.WishRecords.AddAsync(_testWishRecord);
            await _projectDianaContext.SaveChangesAsync();
        }
    }
}
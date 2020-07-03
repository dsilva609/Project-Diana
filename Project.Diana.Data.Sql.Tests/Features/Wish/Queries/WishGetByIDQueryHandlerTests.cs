using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Wish.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Wish.Queries
{
    public class WishGetByIDQueryHandlerTests : DbContextTestBase<ProjectDianaContext>
    {
        private readonly WishGetByIDQueryHandler _handler;
        private readonly ProjectDianaContext _projectDianaContext;
        private readonly WishGetByIDQuery _testQuery;
        private readonly WishRecord _testWishRecord;

        public WishGetByIDQueryHandlerTests()
        {
            var fixture = new Fixture();

            _projectDianaContext = base.InitializeDatabase();
            _testWishRecord = fixture.Create<WishRecord>();
            _testQuery = new WishGetByIDQuery(1, _testWishRecord.ID);

            InitializeDatabase();

            _handler = new WishGetByIDQueryHandler(_projectDianaContext);
        }

        [Fact]
        public async Task HandlerRetrievesWish()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        private async Task InitializeRecords()
        {
            await _projectDianaContext.WishRecords.AddAsync(_testWishRecord);
            await _projectDianaContext.SaveChangesAsync();
        }
    }
}
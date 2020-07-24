using System.Collections.Generic;
using System.Linq;
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
    public class WishGetListByUserIDQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly IFixture _fixture;
        private readonly WishGetListByUserIDQueryHandler _handler;
        private readonly ProjectDianaReadonlyContext _projectDianaContext;
        private readonly WishGetListByUserIDQuery _testQuery;
        private readonly IEnumerable<WishRecord> _wishRecords;

        public WishGetListByUserIDQueryHandlerTests()
        {
            _fixture = new Fixture();

            _projectDianaContext = InitializeDatabase();
            _testQuery = _fixture.Create<WishGetListByUserIDQuery>();
            _wishRecords = CreateWishRecords();

            _handler = new WishGetListByUserIDQueryHandler(_projectDianaContext);
        }

        [Fact]
        public async Task Handler_Returns_List_For_Requested_UserID()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().HaveCount(_wishRecords.Count(w => w.UserID == _testQuery.UserID));
            result.All(r => r.UserID == _testQuery.UserID).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Records()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
        }

        private List<WishRecord> CreateWishRecords()
        {
            var records = new List<WishRecord>
            {
                _fixture.Create<WishRecord>(), _fixture.Create<WishRecord>(), _fixture.Create<WishRecord>()
            };

            foreach (var record in records)
            {
                record.UserID = _testQuery.UserID;
            }

            records.Add(_fixture.Create<WishRecord>());

            return records;
        }

        private async Task InitializeRecords()
        {
            await _projectDianaContext.WishRecords.AddRangeAsync(_wishRecords);

            await _projectDianaContext.SaveChangesAsync();
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Queries
{
    public class AlbumGetLatestAddedQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumGetLatestAddedQueryHandler _handler;
        private readonly AlbumGetLatestAddedQuery _testQuery;

        public AlbumGetLatestAddedQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = new AlbumGetLatestAddedQuery(5);

            _handler = new AlbumGetLatestAddedQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Album_List()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Albums.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_Returns_Requested_Count()
        {
            await InitializeRecords();

            var query = new AlbumGetLatestAddedQuery(1);

            var result = await _handler.Handle(query);

            result.Albums.Should().HaveCount(query.ItemCount);
        }

        [Fact]
        public async Task Handler_Sorts_By_Date_Added()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            var newestAlbum = result.Albums.FirstOrDefault();
            var previousAlbum = result.Albums.ElementAtOrDefault(1);

            newestAlbum.DateAdded.Should().BeAfter(previousAlbum.DateAdded);
        }

        private async Task InitializeRecords()
        {
            await _context.AlbumRecords.AddRangeAsync(_fixture.CreateMany<AlbumRecord>());

            await _context.SaveChangesAsync();
        }
    }
}
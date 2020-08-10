using System.Collections.Generic;
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
    public class AlbumListGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly IEnumerable<AlbumRecord> _albumRecords;
        private readonly ProjectDianaReadonlyContext _context;
        private readonly AlbumListGetQueryHandler _handler;
        private readonly AlbumListGetQuery _testQuery;

        public AlbumListGetQueryHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            _albumRecords = fixture.Create<IEnumerable<AlbumRecord>>();
            _testQuery = fixture.Create<AlbumListGetQuery>();

            _handler = new AlbumListGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Albums()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
        }

        private async Task InitializeRecords()
        {
            await _context.AlbumRecords.AddRangeAsync(_albumRecords);

            await _context.SaveChangesAsync();
        }
    }
}
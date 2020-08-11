using System.Collections.Generic;
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
    public class AlbumListGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly IEnumerable<AlbumRecord> _albumRecords;
        private readonly ProjectDianaReadonlyContext _context;
        private readonly AlbumListGetQueryHandler _handler;
        private readonly AlbumListGetQuery _testQuery;
        private readonly AlbumRecord _testRecord;

        public AlbumListGetQueryHandlerTests()
        {
            var fixture = new Fixture();

            _context = InitializeDatabase();

            _albumRecords = fixture.Create<IEnumerable<AlbumRecord>>();
            _testQuery = fixture.Create<AlbumListGetQuery>();
            _testRecord = fixture
                .Build<AlbumRecord>()
                .With(album => album.UserID, _testQuery.User.Id)
                .Create();

            _handler = new AlbumListGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Album_For_UserID()
        {
            await _context.AlbumRecords.AddAsync(_testRecord);

            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNullOrEmpty();
            result.All(a => a.UserID == _testQuery.User.Id).Should().BeTrue();
        }

        [Fact]
        public async Task Handler_Returns_Albums()
        {
            await _context.AlbumRecords.AddAsync(_testRecord);

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
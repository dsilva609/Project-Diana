using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Context;
using Project.Diana.Data.Sql.Features.Album.Queries;
using Project.Diana.Tests.Common.TestBases;
using Xunit;

namespace Project.Diana.Data.Sql.Tests.Features.Album.Queries
{
    public class AlbumStatsGetQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumStatsGetQueryHandler _handler;
        private readonly AlbumStatsGetQuery _testQuery;

        public AlbumStatsGetQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = new AlbumStatsGetQuery();

            _handler = new AlbumStatsGetQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Gets_33_RPM_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Speed, SpeedReference.RPM33)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.RPM33RecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_45_RPM_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Speed, SpeedReference.RPM45)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.RPM45RecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_78_RPM_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Speed, SpeedReference.RPM78)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.RPM78RecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Album_Count()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.AlbumCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Album_Count_For_User()
        {
            var testQuery = new AlbumStatsGetQuery(1);

            var albums = _fixture
                .Build<AlbumRecord>()
                .With(a => a.UserNum, testQuery.UserId)
                .CreateMany();

            var notIncludedAlbum = _fixture
                .Build<AlbumRecord>()
                .With(a => a.UserNum, testQuery.UserId + 1)
                .Create();

            await _context.AlbumRecords.AddRangeAsync(albums);
            await _context.AlbumRecords.AddAsync(notIncludedAlbum);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(testQuery);

            result.AlbumCount.Should().Be(albums.Count());
        }

        [Fact]
        public async Task Handler_Gets_Albums_In_Progress()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.CompletionStatus, CompletionStatusReference.InProgress)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.InProgressAlbums.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_CD_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.MediaType, MediaTypeReference.CD)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.CDCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Completed_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.CompletionStatus, CompletionStatusReference.Completed)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.CompletedAlbums.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Not_Completed_Albums()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.CompletionStatus, CompletionStatusReference.NotStarted)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.NotCompletedAlbums.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Seven_Inch_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Size, SizeReference.Seven)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.SevenInchRecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Ten_Inch_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Size, SizeReference.Ten)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.TenInchRecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Twelve_Inch_Record_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.Size, SizeReference.Twelve)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.TwelveInchRecordCount.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Handler_Gets_Vinyl_Count()
        {
            await InitializeRecords();

            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.MediaType, MediaTypeReference.Vinyl)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.VinylCount.Should().BeGreaterThan(0);
        }

        private async Task InitializeRecords()
        {
            await _context.AlbumRecords.AddAsync(_fixture.Create<AlbumRecord>());

            await _context.SaveChangesAsync();
        }
    }
}
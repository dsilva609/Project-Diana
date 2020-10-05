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
    public class AlbumGetByIdQueryHandlerTests : DbContextTestBase<ProjectDianaReadonlyContext>
    {
        private readonly ProjectDianaReadonlyContext _context;
        private readonly IFixture _fixture;
        private readonly AlbumGetByIdQueryHandler _handler;
        private readonly AlbumGetByIdQuery _testQuery;

        public AlbumGetByIdQueryHandlerTests()
        {
            _fixture = new Fixture();

            _context = InitializeDatabase();

            _testQuery = _fixture.Create<AlbumGetByIdQuery>();

            _handler = new AlbumGetByIdQueryHandler(_context);
        }

        [Fact]
        public async Task Handler_Returns_Album()
        {
            await InitializeRecords();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_Returns_Album_For_Given_User()
        {
            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.ID, _testQuery.Id)
                .With(a => a.UserID, _testQuery.User.Id)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();

            var result = await _handler.Handle(_testQuery);

            result.Should().NotBeNull();
            result.UserID.Should().Be(_testQuery.User.Id);
        }

        [Fact]
        public async Task Handler_Returns_Album_When_User_Id_Is_Missing()
        {
            await InitializeRecords();

            var query = new AlbumGetByIdQuery(_testQuery.Id, null);

            var result = await _handler.Handle(query);

            result.Should().NotBeNull();
        }

        private async Task InitializeRecords()
        {
            var album = _fixture
                .Build<AlbumRecord>()
                .With(a => a.ID, _testQuery.Id)
                .With(a => a.UserID, _testQuery.User.Id)
                .Create();

            await _context.AlbumRecords.AddAsync(album);

            await _context.SaveChangesAsync();
        }
    }
}
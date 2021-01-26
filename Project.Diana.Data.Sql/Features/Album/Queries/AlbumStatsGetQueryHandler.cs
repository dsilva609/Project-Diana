using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Queries;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Queries
{
    public class AlbumStatsGetQueryHandler : IQueryHandler<AlbumStatsGetQuery, AlbumStats>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public AlbumStatsGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<AlbumStats> Handle(AlbumStatsGetQuery query)
        {
            var albums = query.UserId == 0 ? _context.Albums : _context.Albums.Where(album => album.UserNum == query.UserId);

            var albumCount = await albums.CountAsync();

            var completedAlbums = await albums.CountAsync(a => a.CompletionStatus == CompletionStatusReference.Completed);

            var inProgressAlbums = await albums.CountAsync(a => a.CompletionStatus == CompletionStatusReference.InProgress);

            var notCompleteAlbums = await albums.CountAsync(a => a.CompletionStatus == CompletionStatusReference.NotStarted);

            var cdCount = await albums.CountAsync(a => a.MediaType == MediaTypeReference.CD);

            var vinylCount = await albums.CountAsync(a => a.MediaType == MediaTypeReference.Vinyl);

            var rpm33Count = await albums.CountAsync(a => a.Speed == SpeedReference.RPM33);

            var rpm45Count = await albums.CountAsync(a => a.Speed == SpeedReference.RPM45);

            var rpm78Count = await albums.CountAsync(a => a.Speed == SpeedReference.RPM78);

            var sevenInchCount = await albums.CountAsync(a => a.Size == SizeReference.Seven);

            var tenInchCount = await albums.CountAsync(a => a.Size == SizeReference.Ten);

            var twelveInchCount = await albums.CountAsync(a => a.Size == SizeReference.Twelve);

            return new AlbumStats
            {
                AlbumCount = albumCount,
                CDCount = cdCount,
                CompletedAlbums = completedAlbums,
                InProgressAlbums = inProgressAlbums,
                NotCompletedAlbums = notCompleteAlbums,
                RPM33RecordCount = rpm33Count,
                RPM45RecordCount = rpm45Count,
                RPM78RecordCount = rpm78Count,
                SevenInchRecordCount = sevenInchCount,
                TenInchRecordCount = tenInchCount,
                TwelveInchRecordCount = twelveInchCount,
                VinylCount = vinylCount
            };
        }
    }
}
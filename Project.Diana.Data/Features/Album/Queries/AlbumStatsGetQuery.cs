using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumStatsGetQuery : IQuery<AlbumStats>
    {
        public int UserId { get; }

        public AlbumStatsGetQuery(int userId = 0) => UserId = userId;
    }
}
using System.Collections.Generic;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumListResponse
    {
        public IEnumerable<AlbumRecord> Albums { get; set; }
        public int TotalCount { get; set; }
    }
}
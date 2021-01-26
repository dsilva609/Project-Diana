using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;

namespace Project.Diana.WebApi.Features.Stats
{
    public class StatsResponse
    {
        public AlbumStats AlbumStats { get; set; }
        public BookStats BookStats { get; set; }
        public int CompletedCount { get; set; }
        public int InProgressCount { get; set; }
        public int NotCompletedCount { get; set; }
        public int TotalCount { get; set; }
    }
}
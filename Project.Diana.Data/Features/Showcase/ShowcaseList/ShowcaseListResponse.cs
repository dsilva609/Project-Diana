using System.Collections.Generic;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Book;

namespace Project.Diana.Data.Features.Showcase.ShowcaseList
{
    public class ShowcaseListResponse
    {
        public IEnumerable<AlbumRecord> ShowcasedAlbums { get; set; }
        public IEnumerable<BookRecord> ShowcasedBooks { get; set; }
    }
}
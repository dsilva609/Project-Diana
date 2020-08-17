using System.Collections.Generic;
using Project.Diana.Data.Features.Album;

namespace Project.Diana.Data.Features.Showcase.ShowcaseList
{
    public class ShowcaseListResponse
    {
        public IEnumerable<AlbumRecord> ShowcasedAlbums { get; set; }
    }
}
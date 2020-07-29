using System.Collections.Generic;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.WebApi.Features.Wish.WishList
{
    public class WishList
    {
        public string Category { get; set; }
        public IEnumerable<WishRecord> Wishes { get; set; }
    }

    public class WishListResponse
    {
        public IEnumerable<WishList> AlbumWishes { get; set; }
        public IEnumerable<WishList> BookWishes { get; set; }
        public IEnumerable<WishList> GameWishes { get; set; }
        public IEnumerable<WishList> MovieWishes { get; set; }
    }
}
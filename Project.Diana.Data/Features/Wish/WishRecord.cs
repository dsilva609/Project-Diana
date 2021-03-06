using System;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.Data.Features.Wish
{
    public class WishRecord
    {
        public string ApiId { get; set; }
        public string Category { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public ItemReference ItemType { get; set; }
        public string Notes { get; set; }
        public bool Owned { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
    }
}
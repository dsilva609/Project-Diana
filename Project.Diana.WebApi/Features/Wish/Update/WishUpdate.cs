using Project.Diana.Data.Features.Item;

namespace Project.Diana.WebApi.Features.Wish.Update
{
    public class WishUpdate
    {
        public string ApiID { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public ItemReference ItemType { get; set; }
        public string Notes { get; set; }
        public bool Owned { get; set; }
        public string Title { get; set; }
        public int WishID { get; set; }
    }
}
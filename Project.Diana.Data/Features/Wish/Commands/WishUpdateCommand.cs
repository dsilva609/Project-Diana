using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishUpdateCommand : ICommand
    {
        public string ApiID { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public ItemReference ItemType { get; set; }
        public string Notes { get; set; }
        public bool Owned { get; set; }
        public string Title { get; }
        public string UserID { get; }
        public int WishID { get; }

        public WishUpdateCommand(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            string userID,
            int wishID)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.NullOrWhiteSpace(userID, nameof(userID));
            Guard.Against.Default(wishID, nameof(wishID));

            ApiID = apiID;
            Category = category;
            ImageUrl = imageUrl;
            ItemType = itemType;
            Notes = notes;
            Owned = owned;
            Title = title;
            UserID = userID;
            WishID = wishID;
        }
    }
}
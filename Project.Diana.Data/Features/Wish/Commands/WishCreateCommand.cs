using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishCreateCommand : ICommand
    {
        public string ApiID { get; }
        public string Category { get; }
        public string ImageUrl { get; }
        public ItemReference ItemType { get; }
        public string Notes { get; }
        public bool Owned { get; }
        public string Title { get; }
        public string UserID { get; }

        public WishCreateCommand(
            string apiID,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            string userID)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.NullOrWhiteSpace(userID, nameof(userID));

            ApiID = apiID;
            Category = category;
            ImageUrl = imageUrl;
            ItemType = itemType;
            Notes = notes;
            Owned = owned;
            Title = title;
            UserID = userID;
        }
    }
}
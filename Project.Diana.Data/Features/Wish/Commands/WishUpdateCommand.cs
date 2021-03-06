using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Commands;
using Project.Diana.Data.Features.Item;

namespace Project.Diana.Data.Features.Wish.Commands
{
    public class WishUpdateCommand : ICommand
    {
        public string ApiId { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public ItemReference ItemType { get; set; }
        public string Notes { get; set; }
        public bool Owned { get; set; }
        public string Title { get; }
        public string UserId { get; }
        public int WishId { get; }

        public WishUpdateCommand(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            string userId,
            int wishId)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.NullOrWhiteSpace(userId, nameof(userId));
            Guard.Against.Default(wishId, nameof(wishId));

            ApiId = apiId;
            Category = category;
            ImageUrl = imageUrl;
            ItemType = itemType;
            Notes = notes;
            Owned = owned;
            Title = title;
            UserId = userId;
            WishId = wishId;
        }
    }
}
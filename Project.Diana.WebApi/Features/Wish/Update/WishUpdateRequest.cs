﻿using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Features.Wish.Update
{
    public class WishUpdateRequest : IRequest
    {
        public string ApiId { get; }
        public string Category { get; }
        public string ImageUrl { get; }
        public ItemReference ItemType { get; }
        public string Notes { get; }
        public bool Owned { get; }
        public string Title { get; }
        public ApplicationUser User { get; }
        public int WishId { get; }

        public WishUpdateRequest(
            string apiId,
            string category,
            string imageUrl,
            ItemReference itemType,
            string notes,
            bool owned,
            string title,
            ApplicationUser user,
            int wishId)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(user, nameof(user));
            Guard.Against.NullOrWhiteSpace(user.Id, nameof(user.Id));
            Guard.Against.Default(wishId, nameof(wishId));

            ApiId = apiId;
            Category = category;
            ImageUrl = imageUrl;
            ItemType = itemType;
            Notes = notes;
            Owned = owned;
            Title = title;
            User = user;
            WishId = wishId;
        }
    }
}
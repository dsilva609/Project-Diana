using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumListGetQuery : IQuery<AlbumListResponse>
    {
        public int ItemCount { get; }
        public int Page { get; }
        public ApplicationUser User { get; }

        public AlbumListGetQuery(int itemCount, int page, ApplicationUser user)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));
            Guard.Against.Negative(page, nameof(page));

            ItemCount = itemCount == 0 ? 10 : itemCount;
            Page = page;
            User = user;
        }
    }
}
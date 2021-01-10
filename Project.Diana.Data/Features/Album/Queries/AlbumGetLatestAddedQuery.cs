using Ardalis.GuardClauses;
using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Album.Queries
{
    public class AlbumGetLatestAddedQuery : IQuery<AlbumListResponse>
    {
        public int ItemCount { get; }

        public AlbumGetLatestAddedQuery(int itemCount)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount == 0 ? 10 : itemCount;
        }
    }
}
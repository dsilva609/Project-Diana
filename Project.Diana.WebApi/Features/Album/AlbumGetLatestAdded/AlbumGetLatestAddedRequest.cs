using Ardalis.GuardClauses;
using MediatR;
using Project.Diana.Data.Features.Album.Queries;

namespace Project.Diana.WebApi.Features.Album.AlbumGetLatestAdded
{
    public class AlbumGetLatestAddedRequest : IRequest<AlbumListResponse>
    {
        public int ItemCount { get; }

        public AlbumGetLatestAddedRequest(int itemCount)
        {
            Guard.Against.Negative(itemCount, nameof(itemCount));

            ItemCount = itemCount;
        }
    }
}
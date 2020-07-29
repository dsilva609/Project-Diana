using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.WishList
{
    public class WishGetListByUserIDRequestHandler : IRequestHandler<WishGetListByUserIDRequest, WishListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WishGetListByUserIDRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<WishListResponse> Handle(WishGetListByUserIDRequest request, CancellationToken cancellationToken)
        {
            var wishes = await _queryDispatcher.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(
                new WishGetListByUserIDQuery(request.User.Id));

            var response = new WishListResponse
            {
                AlbumWishes = wishes.Where(w => w.ItemType == ItemReference.Album).GroupBy(g => g.Category).Select(list => new Wish.WishList.WishList { Category = list.Key, Wishes = list.OrderBy(x => x.Title) }),
                BookWishes = wishes.Where(w => w.ItemType == ItemReference.Book).GroupBy(g => g.Category).Select(list => new Wish.WishList.WishList { Category = list.Key, Wishes = list.OrderBy(x => x.Title) }),
                GameWishes = wishes.Where(w => w.ItemType == ItemReference.Game).GroupBy(g => g.Category).Select(list => new Wish.WishList.WishList { Category = list.Key, Wishes = list.OrderBy(x => x.Title) }),
                MovieWishes = wishes.Where(w => w.ItemType == ItemReference.Movie).GroupBy(g => g.Category).Select(list => new Wish.WishList.WishList { Category = list.Key, Wishes = list.OrderBy(x => x.Title) })
            };

            return response;
        }
    }
}
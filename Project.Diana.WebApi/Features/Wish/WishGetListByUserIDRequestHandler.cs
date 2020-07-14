using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetListByUserIDRequestHandler : IRequestHandler<WishGetListByUserIDRequest, IEnumerable<WishRecord>>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WishGetListByUserIDRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<IEnumerable<WishRecord>> Handle(WishGetListByUserIDRequest request, CancellationToken cancellationToken) =>
            await _queryDispatcher.Dispatch<WishGetListByUserIDQuery, IEnumerable<WishRecord>>(
                new WishGetListByUserIDQuery(request.User.Id));
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.Retrieve
{
    public class WishGetByIDRequestHandler : IRequestHandler<WishGetByIDRequest, WishRecord>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WishGetByIDRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<WishRecord> Handle(WishGetByIDRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<WishGetByIDQuery, WishRecord>(new WishGetByIDQuery(request.User.Id, request.WishID));
    }
}
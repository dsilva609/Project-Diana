using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish.Retrieve
{
    public class WishGetByIdRequestHandler : IRequestHandler<WishGetByIdRequest, WishRecord>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WishGetByIdRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<WishRecord> Handle(WishGetByIdRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<WishGetByIdQuery, WishRecord>(new WishGetByIdQuery(request.User.Id, request.WishId));
    }
}
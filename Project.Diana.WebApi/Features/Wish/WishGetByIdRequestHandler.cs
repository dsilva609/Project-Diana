using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Wish.Queries;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Wish
{
    public class WishGetByIdRequestHandler : IRequestHandler<WishGetByIDRequest, string>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public WishGetByIdRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<string> Handle(WishGetByIDRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<WishGetByIDQuery, string>(new WishGetByIDQuery(1, request.ID));
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Diana.Data.Features.Showcase.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;
using Project.Diana.Data.Sql.Bases.Dispatchers;

namespace Project.Diana.WebApi.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequestHandler : IRequestHandler<ShowcaseGetListRequest, ShowcaseListResponse>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public ShowcaseGetListRequestHandler(IQueryDispatcher queryDispatcher) => _queryDispatcher = queryDispatcher;

        public async Task<ShowcaseListResponse> Handle(ShowcaseGetListRequest request, CancellationToken cancellationToken)
            => await _queryDispatcher.Dispatch<ShowcaseGetListQuery, ShowcaseListResponse>(
                new ShowcaseGetListQuery(request.User));
    }
}
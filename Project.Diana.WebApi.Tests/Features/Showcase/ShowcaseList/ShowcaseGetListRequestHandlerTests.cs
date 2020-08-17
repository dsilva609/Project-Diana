using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Project.Diana.Data.Features.Showcase.Queries;
using Project.Diana.Data.Features.Showcase.ShowcaseList;
using Project.Diana.Data.Sql.Bases.Dispatchers;
using Project.Diana.WebApi.Features.Showcase.ShowcaseList;
using Xunit;

namespace Project.Diana.WebApi.Tests.Features.Showcase.ShowcaseList
{
    public class ShowcaseGetListRequestHandlerTests
    {
        private readonly ShowcaseGetListRequestHandler _handler;
        private readonly Mock<IQueryDispatcher> _queryDispatcher;
        private readonly ShowcaseGetListRequest _testRequest;

        public ShowcaseGetListRequestHandlerTests()
        {
            var fixture = new Fixture();

            _queryDispatcher = new Mock<IQueryDispatcher>();
            _testRequest = fixture.Create<ShowcaseGetListRequest>();

            _handler = new ShowcaseGetListRequestHandler(_queryDispatcher.Object);
        }

        [Fact]
        public async Task Handler_Calls_Query_Dispatcher()
        {
            await _handler.Handle(_testRequest, CancellationToken.None);

            _queryDispatcher.Verify(x => x.Dispatch<ShowcaseGetListQuery, ShowcaseListResponse>(It.IsNotNull<ShowcaseGetListQuery>()), Times.Once);
        }
    }
}
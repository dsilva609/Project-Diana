using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Project.Diana.ApiClient.Features.ComicVine;
using RestSharp;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.ComicVine
{
    public class ComicVineApiClientTests
    {
        private readonly IComicVineApiClient _apiClient;
        private readonly Mock<IRestClient> _restClient;

        public ComicVineApiClientTests()
        {
            var fixture = new Fixture();

            var configuration = fixture
                .Build<ComicVineApiClientConfiguration>()
                .With(c => c.BaseUrl, "https://comicvine.gamespot.com")
                .Create();

            _restClient = new Mock<IRestClient>();

            _apiClient = new ComicVineApiClient(configuration, _restClient.Object);
        }

        [Theory, AutoData]
        public async Task Send_Search_Request_Returns_Failure_If_Request_Fails(string title)
        {
            _restClient.Setup(x => x.ExecuteAsync<ComicVineSearchResult>(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse<ComicVineSearchResult>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "failure"
                });

            var result = await _apiClient.SendSearchRequest(title);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Send_Search_Request_Returns_Failure_If_Response_Is_Null(string title)
        {
            _restClient.Setup(x => x.ExecuteAsync<ComicVineSearchResult>(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse<ComicVineSearchResult>
                {
                    StatusCode = HttpStatusCode.OK,
                    ResponseStatus = ResponseStatus.Completed,
                    Data = null
                });

            var result = await _apiClient.SendSearchRequest(title);

            result.IsFailure.Should().BeTrue();
        }
    }
}
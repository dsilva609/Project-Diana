using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Project.Diana.ApiClient.Features.Discogs;
using RestSharp;
using Xunit;

namespace Project.Diana.ApiClient.Tests.Features.Discogs
{
    public class DiscogsApiClientTests
    {
        private readonly IDiscogsApiClient _apiClient;
        private readonly Mock<IRestClient> _restClient;

        public DiscogsApiClientTests()
        {
            var fixture = new Fixture();

            var configuration = fixture
                .Build<DiscogsApiClientConfiguration>()
                .With(c => c.BaseUrl, "https://api.discogs.com")
                .Create();

            _restClient = new Mock<IRestClient>();

            _apiClient = new DiscogsApiClient(configuration, _restClient.Object);
        }

        [Theory, AutoData]
        public async Task Send_Search_Request_Returns_Failure_If_Request_Fails(string artist, string album)
        {
            _restClient.Setup(x => x.ExecuteAsync<DiscogsSearchResult>(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new RestResponse<DiscogsSearchResult> { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = "failure" });

            var result = await _apiClient.SendSearchRequest(artist, album);

            result.IsFailure.Should().BeTrue();
        }

        [Theory, AutoData]
        public async Task Send_Search_Request_Returns_Failure_If_Request_Response_Is_Null(string artist, string album)
        {
            _restClient.Setup(x => x.ExecuteAsync<DiscogsSearchResult>(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse<DiscogsSearchResult> { StatusCode = HttpStatusCode.OK, ResponseStatus = ResponseStatus.Completed, Data = null });

            var result = await _apiClient.SendSearchRequest(artist, album);

            result.IsFailure.Should().BeTrue();
        }
    }
}
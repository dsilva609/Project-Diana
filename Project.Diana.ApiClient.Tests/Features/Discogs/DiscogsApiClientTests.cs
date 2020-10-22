using AutoFixture;
using Moq;
using Project.Diana.ApiClient.Features.Discogs;
using RestSharp;

namespace Project.Diana.ApiClient.Tests.Features.Discogs
{
    public class DiscogsApiClientTests
    {
        private readonly IDiscogsApiClient _apiClient;
        private readonly Mock<IRestClient> _restClient;

        public DiscogsApiClientTests()
        {
            var fixture = new Fixture();

            var configuration = fixture.Create<IDiscogsApiClientConfiguration>();
            _restClient = new Mock<IRestClient>();

            _apiClient = new DiscogsApiClient(configuration, _restClient.Object);
        }
    }
}
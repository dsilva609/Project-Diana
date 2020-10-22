using Moq;
using Project.Diana.ApiClient.Features.Discogs;
using Project.Diana.Provider.Features.Discogs;

namespace Project.Diana.Provider.Tests.Features.Discogs
{
    public class DiscogsProviderTests
    {
        private readonly Mock<IDiscogsApiClient> _apiClient;
        private readonly IDiscogsProvider _provider;

        public DiscogsProviderTests()
        {
            _apiClient = new Mock<IDiscogsApiClient>();

            _provider = new DiscogsProvider(_apiClient.Object);
        }
    }
}
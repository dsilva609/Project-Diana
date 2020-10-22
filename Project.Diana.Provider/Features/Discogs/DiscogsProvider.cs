using Project.Diana.ApiClient.Features.Discogs;

namespace Project.Diana.Provider.Features.Discogs
{
    public class DiscogsProvider : IDiscogsProvider
    {
        private readonly IDiscogsApiClient _apiClient;

        public DiscogsProvider(IDiscogsApiClient apiClient) => _apiClient = apiClient;

        public void GetReleaseFromId(int releaseId) => throw new System.NotImplementedException();

        public void SearchForAlbum(string artist, string album) => throw new System.NotImplementedException();
    }
}
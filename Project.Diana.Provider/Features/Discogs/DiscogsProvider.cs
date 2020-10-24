using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Project.Diana.ApiClient.Features.Discogs;

namespace Project.Diana.Provider.Features.Discogs
{
    public class DiscogsProvider : IDiscogsProvider
    {
        private readonly IDiscogsApiClient _apiClient;

        public DiscogsProvider(IDiscogsApiClient apiClient) => _apiClient = apiClient;

        public void GetReleaseFromId(int releaseId) => throw new System.NotImplementedException();

        public async Task<Result<IEnumerable<SearchResult>>> SearchForAlbum(string album, string artist)
        {
            if (string.IsNullOrWhiteSpace(album) && string.IsNullOrWhiteSpace(artist))
            {
                return Result.Failure<IEnumerable<SearchResult>>("Artist and album are missing.");
            }

            var result = await _apiClient.SendSearchRequest(album, artist);

            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<SearchResult>>($"Unable to retrieve result - {result.Error}");
            }

            var searchResults = result.Value;

            if (!searchResults.results.Any())
            {
                return Result.Failure<IEnumerable<SearchResult>>("Response returned no results");
            }

            return Result.Success(searchResults.results);
        }
    }
}
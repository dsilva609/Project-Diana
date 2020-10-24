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

        public async Task<Result<IEnumerable<AlbumSearchResponse>>> SearchForAlbum(string album, string artist)
        {
            if (string.IsNullOrWhiteSpace(album) && string.IsNullOrWhiteSpace(artist))
            {
                return Result.Failure<IEnumerable<AlbumSearchResponse>>("Artist and album are missing.");
            }

            var result = await _apiClient.SendSearchRequest(album, artist);

            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<AlbumSearchResponse>>($"Unable to retrieve result - {result.Error}");
            }

            var searchResults = result.Value;

            if (!searchResults.results.Any())
            {
                return Result.Failure<IEnumerable<AlbumSearchResponse>>("Response returned no results");
            }

            var results = searchResults.results.Select(r => new AlbumSearchResponse
            {
                Id = r.id,
                Artist = r.title.Substring(0, r.title.IndexOf('-')).Trim(),
                CountryOfOrigin = r.country,
                CoverImage = r.cover_image,
                Format = string.Join(", ", r.format),
                Genre = string.Join(", ", r.genre),
                RecordLabel = r.label.FirstOrDefault(),
                Style = string.Join(", ", r.style),
                Thumbnail = r.thumb,
                Title = r.title.Substring(r.title.IndexOf('-') + 1).Trim(),
                YearReleased = r.year
            });

            return Result.Success(results);
        }
    }
}
using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;

namespace Project.Diana.ApiClient.Features.GoogleBooks
{
    public class GoogleBooksApiClient : IGoogleBooksApiClient
    {
        private readonly VolumesResource _volumesResource;

        public GoogleBooksApiClient(VolumesResource volumesResource) => _volumesResource = volumesResource;

        public async Task<Result<Volumes>> Search(string author, string title)
        {
            var query = string.Empty;

            if (!string.IsNullOrWhiteSpace(author))
            {
                query += $"inauthor:{author}";
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $"+intitle:{title}";
            }

            var searchRequest = _volumesResource.List();
            searchRequest.Q = query;

            Volumes volumes;

            try
            {
                volumes = await searchRequest.ExecuteAsync();
            }
            catch (Exception e)
            {
                return Result.Failure<Volumes>($"Error retrieving results: {e.Message}");
            }

            return Result.Success(volumes);
        }
    }
}
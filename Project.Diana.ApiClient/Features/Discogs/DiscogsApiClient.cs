using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RestSharp;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public class DiscogsApiClient : IDiscogsApiClient
    {
        private readonly IDiscogsApiClientConfiguration _configuration;
        private readonly IRestClient _restClient;

        public DiscogsApiClient(IDiscogsApiClientConfiguration configuration, IRestClient restClient)
        {
            _configuration = configuration;
            _restClient = restClient;

            _restClient.BaseUrl = new Uri(_configuration.BaseUrl);
        }

        public async Task<Result<DiscogsSearchResult>> SendSearchRequest(string album, string artist)
        {
            var request = new RestRequest(_configuration.SearchResource, Method.GET);

            request.AddHeader("Authorization", $"Discogs token={_configuration.DiscogsToken}");
            request.AddQueryParameter("type", "release");
            request.AddQueryParameter("q", $"{artist}+{album}");

            var result = await _restClient.ExecuteAsync<DiscogsSearchResult>(request);

            if (!result.IsSuccessful)
            {
                return Result.Failure<DiscogsSearchResult>(result.ErrorMessage);
            }

            var searchResult = result.Data;

            return searchResult is null
                ? Result.Failure<DiscogsSearchResult>("Response returned no data.")
                : Result.Success(searchResult);
        }
    }
}
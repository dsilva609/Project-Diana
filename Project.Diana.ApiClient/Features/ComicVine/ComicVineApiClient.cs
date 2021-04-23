using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RestSharp;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public class ComicVineApiClient : IComicVineApiClient
    {
        private readonly IComicVineApiClientConfiguration _configuration;
        private readonly IRestClient _restClient;

        public ComicVineApiClient(IComicVineApiClientConfiguration configuration, IRestClient restClient)
        {
            _configuration = configuration;
            _restClient = restClient;

            _restClient.BaseUrl = new Uri(_configuration.BaseUrl);
        }

        public async Task<Result<ComicVineSearchResult>> SendSearchRequest(string title)
        {
            var request = new RestRequest(_configuration.SearchResource, Method.GET);

            request.AddQueryParameter("api_key", _configuration.ApiKey);
            request.AddQueryParameter("format", _configuration.Format);
            request.AddQueryParameter("limit", _configuration.Limit.ToString());
            request.AddQueryParameter("resources", _configuration.ResourceType);
            request.AddQueryParameter("query", title);

            var result = await _restClient.ExecuteAsync<ComicVineSearchResult>(request);

            if (!result.IsSuccessful)
            {
                return Result.Failure<ComicVineSearchResult>(result.ErrorMessage);
            }

            var searchResult = result.Data;

            return searchResult is null
                ? Result.Failure<ComicVineSearchResult>("Response returned no data.")
                : Result.Success(searchResult);
        }
    }
}
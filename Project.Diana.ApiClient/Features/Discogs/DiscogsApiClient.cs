using System;
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

        public void SendGetReleaseRequest(int releaseId) => throw new NotImplementedException();

        public void SendSearchForArtistRequest(string artist, string album) => throw new NotImplementedException();
    }
}
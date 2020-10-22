using Ardalis.GuardClauses;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public class DiscogsApiClientConfiguration : IDiscogsApiClientConfiguration
    {
        public string BaseUrl { get; }
        public string DiscogsToken { get; }
        public string UserAgent { get; }

        public DiscogsApiClientConfiguration(
            string baseUrl,
            string discogsToken,
            string userAgent)
        {
            Guard.Against.NullOrWhiteSpace(baseUrl, nameof(baseUrl));
            Guard.Against.NullOrWhiteSpace(discogsToken, nameof(discogsToken));
            Guard.Against.NullOrWhiteSpace(userAgent, nameof(userAgent));

            BaseUrl = baseUrl;
            DiscogsToken = discogsToken;
            UserAgent = userAgent;
        }
    }
}
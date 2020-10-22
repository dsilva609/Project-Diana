using Project.Diana.ApiClient.Bases;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public interface IDiscogsApiClientConfiguration : IApiClientConfiguration
    {
        string DiscogsToken { get; }
        string UserAgent { get; }
    }
}
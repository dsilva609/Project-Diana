using Project.Diana.ApiClient.Bases;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public interface IComicVineApiClientConfiguration : IApiClientConfiguration
    {
        string ApiKey { get; set; }
        string Format { get; set; }
        string IssueResource { get; set; }
        int Limit { get; set; }
        string ResourceType { get; set; }
        string SearchResource { get; set; }
    }
}
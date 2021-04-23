using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public interface IDiscogsApiClient
    {
        Task<Result<DiscogsSearchResult>> SendSearchRequest(string album, string artist);
    }
}
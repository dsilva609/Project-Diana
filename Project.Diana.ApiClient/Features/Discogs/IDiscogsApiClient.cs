using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.ApiClient.Features.Discogs
{
    public interface IDiscogsApiClient
    {
        void SendGetReleaseRequest(int releaseId);

        Task<Result<DiscogsSearchResult>> SendSearchRequest(string album, string artist);
    }
}
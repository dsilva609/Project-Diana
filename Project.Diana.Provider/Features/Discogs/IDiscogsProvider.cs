using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Project.Diana.ApiClient.Features.Discogs;

namespace Project.Diana.Provider.Features.Discogs
{
    public interface IDiscogsProvider
    {
        void GetReleaseFromId(int releaseId);

        Task<Result<IEnumerable<SearchResult>>> SearchForAlbum(string artist, string album);
    }
}
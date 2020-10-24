using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.Provider.Features.Discogs
{
    public interface IDiscogsProvider
    {
        void GetReleaseFromId(int releaseId);

        Task<Result<IEnumerable<AlbumSearchResponse>>> SearchForAlbum(string album, string artist);
    }
}
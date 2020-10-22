namespace Project.Diana.Provider.Features.Discogs
{
    public interface IDiscogsProvider
    {
        void GetReleaseFromId(int releaseId);

        void SearchForAlbum(string artist, string album);
    }
}
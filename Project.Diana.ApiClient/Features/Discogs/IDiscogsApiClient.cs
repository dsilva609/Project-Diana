namespace Project.Diana.ApiClient.Features.Discogs
{
    public interface IDiscogsApiClient
    {
        void SendGetReleaseRequest(int releaseId);

        void SendSearchForArtistRequest(string artist, string album);
    }
}
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public interface IComicVineApiClient
    {
        Task<Result<ComicVineSearchResult>> SendSearchRequest(string title);
    }
}
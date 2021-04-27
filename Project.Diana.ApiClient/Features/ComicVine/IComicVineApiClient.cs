using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.ApiClient.Features.ComicVine
{
    public interface IComicVineApiClient
    {
        Task<Result<ComicVineIssueDetailsResult>> SendIssueDetailsRequest(string issueId);

        Task<Result<ComicVineSearchResult>> SendSearchRequest(string title);
    }
}
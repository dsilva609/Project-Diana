using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.Provider.Features.ComicVine
{
    public interface IComicVineProvider
    {
        Task<Result<BookSearchResponse>> GetIssueDetails(string issueId);

        Task<Result<IEnumerable<BookSearchResponse>>> SearchForComic(string title);
    }
}
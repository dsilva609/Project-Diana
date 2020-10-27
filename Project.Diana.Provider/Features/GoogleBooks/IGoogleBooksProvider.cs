using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.Provider.Features.GoogleBooks
{
    public interface IGoogleBooksProvider
    {
        Task<Result<BookSearchResponse>> GetVolumeById(string id);

        Task<Result<IEnumerable<BookSearchResponse>>> Search(string author, string title);
    }
}
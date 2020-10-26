using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Project.Diana.Provider.Features.GoogleBooks
{
    public interface IGoogleBooksProvider
    {
        Task<Result<IEnumerable<BookSearchResponse>>> Search(string author, string title);
    }
}
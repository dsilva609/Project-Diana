using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Google.Apis.Books.v1.Data;

namespace Project.Diana.ApiClient.Features.GoogleBooks
{
    public interface IGoogleBooksApiClient
    {
        Task<Result<Volumes>> Search(string author, string title);
    }
}
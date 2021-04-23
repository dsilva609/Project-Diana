using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Project.Diana.ApiClient.Features.ComicVine;
using Project.Diana.Provider.Features.GoogleBooks;

namespace Project.Diana.Provider.Features.ComicVine
{
    public class ComicVineProvider : IComicVineProvider
    {
        private readonly IComicVineApiClient _apiClient;

        public ComicVineProvider(IComicVineApiClient apiClient) => _apiClient = apiClient;

        public async Task<Result<IEnumerable<BookSearchResponse>>> SearchForComic(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<IEnumerable<BookSearchResponse>>("Title is missing");
            }

            var result = await _apiClient.SendSearchRequest(title);

            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<BookSearchResponse>>($"Unable to retrieve result - {result.Error}");
            }

            var searchResults = result.Value;

            var results = searchResults
                .results
                .Select(r => new BookSearchResponse
                {
                    Id = r.id.ToString(),
                    ImageUrl = r.image.super_url,
                    Title = $"{r.name} #{r.issue_number}",
                    YearReleased = DateTime.TryParse(r.store_date, out var releaseDate) ? releaseDate.Year : DateTime.UtcNow.Year
                });

            return Result.Success(results);
        }
    }
}
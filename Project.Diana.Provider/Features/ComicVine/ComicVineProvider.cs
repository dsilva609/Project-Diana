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

        public async Task<Result<BookSearchResponse>> GetIssueDetails(string issueId)
        {
            if (string.IsNullOrWhiteSpace(issueId))
            {
                return Result.Failure<BookSearchResponse>("Issue id is missing");
            }

            var result = await _apiClient.SendIssueDetailsRequest(issueId);

            if (result.IsFailure)
            {
                return Result.Failure<BookSearchResponse>($"Unable to retrieve issue details - {result.Error}");
            }

            var issueDetails = result.Value;

            if (issueDetails?.results is null)
            {
                return Result.Failure<BookSearchResponse>("Client returned no data.");
            }

            var details = issueDetails.results;

            var bookResponse = new BookSearchResponse
            {
                Id = details.ToString(),
                Author = string.Join(",", details.person_credits.Where(person => person.role.Contains("writer")).Select(credit => credit.name)),
                ImageUrl = details.image.original_url,
                Title = $"{details.volume.name} #{details.issue_number}",
                YearReleased = DateTime.TryParse(details.store_date, out var releaseDate) ? releaseDate.Year : DateTime.UtcNow.Year
            };

            return Result.Success(bookResponse);
        }

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
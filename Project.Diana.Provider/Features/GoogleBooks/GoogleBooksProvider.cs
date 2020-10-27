using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Project.Diana.ApiClient.Features.GoogleBooks;

namespace Project.Diana.Provider.Features.GoogleBooks
{
    public class GoogleBooksProvider : IGoogleBooksProvider
    {
        private readonly IGoogleBooksApiClient _apiClient;

        public GoogleBooksProvider(IGoogleBooksApiClient apiClient) => _apiClient = apiClient;

        public async Task<Result<IEnumerable<BookSearchResponse>>> Search(string author, string title)
        {
            if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(title))
            {
                return Result.Failure<IEnumerable<BookSearchResponse>>("Author and title are missing");
            }

            var searchResult = await _apiClient.Search(author, title);

            if (searchResult.IsFailure)
            {
                return Result.Failure<IEnumerable<BookSearchResponse>>($"Book search failed: {searchResult.Error}");
            }

            var results = searchResult.Value.Items.Select(v => new BookSearchResponse
            {
                Id = v.Id,
                Author = v.VolumeInfo.Authors != null ? string.Join(", ", v.VolumeInfo.Authors) : string.Empty,
                CountryOfOrigin = v.SaleInfo?.Country,
                Genre = v.VolumeInfo.Categories != null ? string.Join(", ", v.VolumeInfo.Categories) : string.Empty,
                ImageUrl = !string.IsNullOrWhiteSpace(v.VolumeInfo?.ImageLinks?.Medium) ? v.VolumeInfo?.ImageLinks?.Medium?.Replace("http:", "https:") : v.VolumeInfo?.ImageLinks?.SmallThumbnail?.Replace("http:", "https:"),
                Isbn10 = v.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_10")?.Identifier,
                Isbn13 = v.VolumeInfo.IndustryIdentifiers?.FirstOrDefault(x => x.Type == "ISBN_13")?.Identifier,
                Language = v.VolumeInfo.Language,
                PageCount = v.VolumeInfo.PageCount.GetValueOrDefault(),
                Publisher = v.VolumeInfo.Publisher,
                Title = v.VolumeInfo.Title,
                YearReleased = string.IsNullOrWhiteSpace(v.VolumeInfo.PublishedDate) ? DateTime.UtcNow.Year : Convert.ToInt32(v.VolumeInfo.PublishedDate.Substring(0, 4))
            });

            return Result.Success(results);
        }
    }
}
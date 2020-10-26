namespace Project.Diana.Provider.Features.GoogleBooks
{
    public class BookSearchResponse
    {
        public string Author { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Genre { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public int YearReleased { get; set; }
    }
}
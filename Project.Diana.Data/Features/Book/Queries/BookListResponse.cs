using System.Collections.Generic;

namespace Project.Diana.Data.Features.Book.Queries
{
    public class BookListResponse
    {
        public IEnumerable<BookRecord> Books { get; set; }
        public int TotalCount { get; set; }
    }
}
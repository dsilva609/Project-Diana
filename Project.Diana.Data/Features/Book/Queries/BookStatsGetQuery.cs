using Project.Diana.Data.Bases.Queries;

namespace Project.Diana.Data.Features.Book.Queries
{
    public class BookStatsGetQuery : IQuery<BookStats>
    {
        public int UserId { get; }

        public BookStatsGetQuery(int userId = 0) => UserId = userId;
    }
}
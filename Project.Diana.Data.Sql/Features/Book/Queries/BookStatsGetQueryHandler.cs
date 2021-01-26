using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Queries
{
    public class BookStatsGetQueryHandler : IQueryHandler<BookStatsGetQuery, BookStats>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public BookStatsGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<BookStats> Handle(BookStatsGetQuery query)
        {
            var books = query.UserId == 0 ? _context.Books : _context.Books.Where(b => b.UserNum == query.UserId);

            var bookCount = await books.CountAsync();

            var completedBookCount = await books.CountAsync(b => b.CompletionStatus == CompletionStatusReference.Completed);

            var inProgressBookCount = await books.CountAsync(b => b.CompletionStatus == CompletionStatusReference.InProgress);

            var notCompletedBookCount = await books.CountAsync(b => b.CompletionStatus == CompletionStatusReference.NotStarted);

            var comicCount = await books.CountAsync(b => b.Type == BookTypeReference.Comic);

            var mangaCount = await books.CountAsync(b => b.Type == BookTypeReference.Manga);

            var novelCount = await books.CountAsync(b => b.Type == BookTypeReference.Novel);

            return new BookStats
            {
                BookCount = bookCount,
                ComicCount = comicCount,
                CompletedBookCount = completedBookCount,
                InProgressBookCount = inProgressBookCount,
                MangaCount = mangaCount,
                NotStartedBookCount = notCompletedBookCount,
                NovelCount = novelCount
            };
        }
    }
}
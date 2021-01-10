using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Queries
{
    public class BookGetLatestAddedQueryHandler : IQueryHandler<BookGetLatestAddedQuery, BookListResponse>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public BookGetLatestAddedQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<BookListResponse> Handle(BookGetLatestAddedQuery query)
        {
            var books = await _context.Books
                .OrderByDescending(book => book.DateAdded)
                .Take(query.ItemCount)
                .ToListAsync();

            return new BookListResponse { Books = books, TotalCount = books.Count };
        }
    }
}
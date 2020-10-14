using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Queries
{
    public class BookListGetQueryHandler : IQueryHandler<BookListGetQuery, BookListResponse>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public BookListGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<BookListResponse> Handle(BookListGetQuery query)
        {
            var bookQuery = _context.Books;

            if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            {
                bookQuery = bookQuery.Where(q
                    => q.Author.ToLower().Contains(query.SearchQuery.ToLower())
                       || q.Title.ToLower().Contains(query.SearchQuery.ToLower()));
            }

            var totalCount = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await bookQuery.CountAsync()
                : await bookQuery.CountAsync(book => book.UserID == query.User.Id);

            bookQuery = bookQuery.OrderBy(book => book.Author).ThenBy(book => book.Title).Skip(query.ItemCount * query.Page);

            var books = string.IsNullOrWhiteSpace(query.User?.Id)
                ? await bookQuery.Take(query.ItemCount).ToListAsync()
                : await bookQuery.Where(b => b.UserID == query.User.Id).Take(query.ItemCount).ToListAsync();

            return new BookListResponse
            {
                Books = books,
                TotalCount = totalCount
            };
        }
    }
}
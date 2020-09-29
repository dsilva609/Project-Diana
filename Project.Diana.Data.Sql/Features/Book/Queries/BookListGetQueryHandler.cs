using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Queries
{
    public class BookListGetQueryHandler : IQueryHandler<BookListGetQuery, IEnumerable<BookRecord>>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public BookListGetQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<IEnumerable<BookRecord>> Handle(BookListGetQuery query)
        {
            var bookQuery = _context.Books.OrderBy(book => book.Author).ThenBy(book => book.Title);

            if (string.IsNullOrWhiteSpace(query.User?.Id))
            {
                return await bookQuery.Take(query.ItemCount).ToListAsync();
            }

            return await bookQuery.Where(b => b.UserID == query.User.Id).Take(query.ItemCount).ToListAsync();
        }
    }
}
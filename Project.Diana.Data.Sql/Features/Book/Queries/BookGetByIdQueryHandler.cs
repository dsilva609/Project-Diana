using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Queries
{
    public class BookGetByIdQueryHandler : IQueryHandler<BookGetByIdQuery, BookRecord>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public BookGetByIdQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<BookRecord> Handle(BookGetByIdQuery query)
            => string.IsNullOrWhiteSpace(query.User?.Id)
                ? await _context.Books.FirstOrDefaultAsync(book => book.ID == query.ID)
                : await _context.Books.FirstOrDefaultAsync(book => book.ID == query.ID && book.UserID == query.User.Id);
    }
}
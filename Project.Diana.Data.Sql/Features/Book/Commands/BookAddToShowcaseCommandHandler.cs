using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Commands
{
    public class BookAddToShowcaseCommandHandler : ICommandHandler<BookAddToShowcaseCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public BookAddToShowcaseCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(BookAddToShowcaseCommand command)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b
                => b.ID == command.BookId
                   && b.UserID == command.User.Id);

            if (book is null)
            {
                return;
            }

            if (book.IsShowcased)
            {
                return;
            }

            book.IsShowcased = true;
            book.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
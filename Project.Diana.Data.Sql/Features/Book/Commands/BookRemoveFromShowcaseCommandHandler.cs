using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Commands
{
    public class BookRemoveFromShowcaseCommandHandler : ICommandHandler<BookRemoveFromShowcaseCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public BookRemoveFromShowcaseCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(BookRemoveFromShowcaseCommand command)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b
                => b.Id == command.BookId
                && b.UserId == command.User.Id);

            if (book is null)
            {
                return;
            }

            if (!book.IsShowcased)
            {
                return;
            }

            book.IsShowcased = false;
            book.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
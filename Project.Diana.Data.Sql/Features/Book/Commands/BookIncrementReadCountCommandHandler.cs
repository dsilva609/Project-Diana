using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Commands
{
    public class BookIncrementReadCountCommandHandler : ICommandHandler<BookIncrementReadCountCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public BookIncrementReadCountCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(BookIncrementReadCountCommand command)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b
                => b.Id == command.BookId
                   && b.UserId == command.User.Id);

            if (book is null)
            {
                return;
            }

            book.TimesCompleted++;

            if (book.CompletionStatus != CompletionStatusReference.Completed)
            {
                book.CompletionStatus = CompletionStatusReference.Completed;
            }

            var dateUpdated = DateTime.UtcNow;

            book.LastCompleted = dateUpdated;
            book.DateUpdated = dateUpdated;

            await _context.SaveChangesAsync();
        }
    }
}
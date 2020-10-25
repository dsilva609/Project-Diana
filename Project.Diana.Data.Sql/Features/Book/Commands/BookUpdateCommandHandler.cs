using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Commands
{
    public class BookUpdateCommandHandler : ICommandHandler<BookUpdateCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public BookUpdateCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(BookUpdateCommand command)
        {
            var sameBookExists = await _context.Books.AnyAsync(b
                => b.ID != command.BookId
                && b.Author.ToUpper() == command.Author.ToUpper()
                && b.Type == command.Type
                && b.Title.ToUpper() == command.Title.ToUpper()
                && b.UserID.ToUpper() == command.User.Id.ToUpper());

            if (sameBookExists)
            {
                return;
            }

            var existingBook = await _context.Books.FirstOrDefaultAsync(b
                => b.ID == command.BookId
                 && b.UserID.ToUpper() == command.User.Id.ToUpper());

            if (existingBook is null)
            {
                return;
            }

            var dateUpdated = DateTime.UtcNow;

            existingBook.Author = command.Author;
            existingBook.Category = command.Category;
            existingBook.CompletionStatus = command.CompletionStatus;
            existingBook.CountryOfOrigin = command.CountryOfOrigin;
            existingBook.CountryPurchased = command.CountryPurchased;
            existingBook.DatePurchased = command.DatePurchased;
            existingBook.Genre = command.Genre;
            existingBook.ImageUrl = command.ImageUrl;
            existingBook.ISBN10 = command.ISBN10;
            existingBook.ISBN13 = command.ISBN13;
            existingBook.IsFirstEdition = command.IsFirstEdition;
            existingBook.Hardcover = command.IsHardcover;
            existingBook.IsNew = command.IsNewPurchase;
            existingBook.IsPhysical = command.IsPhysical;
            existingBook.IsReissue = command.IsReissue;
            existingBook.Language = command.Language;
            existingBook.LocationPurchased = command.LocationPurchased;
            existingBook.Notes = command.Notes;
            existingBook.PageCount = command.PageCount;
            existingBook.Publisher = command.Publisher;
            existingBook.TimesCompleted = command.TimesCompleted;
            existingBook.Title = command.Title;
            existingBook.Type = command.Type;
            existingBook.YearReleased = command.YearReleased;

            if (existingBook.TimesCompleted > 0 && existingBook.CompletionStatus != CompletionStatusReference.Completed)
            {
                existingBook.CompletionStatus = CompletionStatusReference.Completed;
                existingBook.LastCompleted = dateUpdated;
            }

            if (existingBook.CompletionStatus == CompletionStatusReference.Completed && existingBook.TimesCompleted == 0)
            {
                existingBook.TimesCompleted = 1;
                existingBook.LastCompleted = dateUpdated;
            }

            existingBook.DateUpdated = dateUpdated;

            await _context.SaveChangesAsync();
        }
    }
}
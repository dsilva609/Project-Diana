using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Diana.Data.Features.Book;
using Project.Diana.Data.Features.Book.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Book.Commands
{
    public class BookSubmissionCommandHandler : ICommandHandler<BookSubmissionCommand>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly IMapper _mapper;

        public BookSubmissionCommandHandler(IProjectDianaWriteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(BookSubmissionCommand command)
        {
            var bookExists = _context.Books.Any(b
                => b.Author.ToUpper() == command.Author.ToUpper()
                && b.Type == command.Type
                && b.Title.ToUpper() == command.Title.ToUpper()
                && b.UserId.ToUpper() == command.User.Id.ToUpper());

            if (bookExists)
            {
                return;
            }

            var newRecord = _mapper.Map<BookRecord>(command);

            if (newRecord.TimesCompleted > 0)
            {
                newRecord.CompletionStatus = CompletionStatusReference.Completed;
            }

            var dateAdded = DateTime.UtcNow;

            newRecord.DateAdded = dateAdded;
            newRecord.DateUpdated = dateAdded;

            await _context.Books.AddAsync(newRecord);

            await _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Diana.Data.Features.Album;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumSubmissionCommandHandler : ICommandHandler<AlbumSubmissionCommand>
    {
        private readonly IProjectDianaWriteContext _context;
        private readonly IMapper _mapper;

        public AlbumSubmissionCommandHandler(IProjectDianaWriteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(AlbumSubmissionCommand command)
        {
            var albumExists = _context.Albums.Any(a
                => a.Artist.ToUpper() == command.Artist.ToUpper()
                   && a.MediaType == command.MediaType
                   && a.Title.ToUpper() == command.Title.ToUpper()
                   && a.UserId.ToUpper() == command.User.Id.ToUpper());

            if (albumExists)
            {
                return;
            }

            var newRecord = _mapper.Map<AlbumRecord>(command);

            if (newRecord.TimesCompleted > 0)
            {
                newRecord.CompletionStatus = CompletionStatusReference.Completed;
            }

            var dateAdded = DateTime.UtcNow;

            newRecord.DateAdded = dateAdded;
            newRecord.DateUpdated = dateAdded;

            await _context.Albums.AddAsync(newRecord);

            await _context.SaveChangesAsync();
        }
    }
}
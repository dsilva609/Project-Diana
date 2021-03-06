using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumIncrementPlayCountCommandHandler : ICommandHandler<AlbumIncrementPlayCountCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public AlbumIncrementPlayCountCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(AlbumIncrementPlayCountCommand command)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(a
                => a.Id == command.AlbumId
                 && a.UserId == command.User.Id);

            if (album is null)
            {
                return;
            }

            album.TimesCompleted++;

            if (album.CompletionStatus != CompletionStatusReference.Completed)
            {
                album.CompletionStatus = CompletionStatusReference.Completed;
            }

            var dateUpdated = DateTime.UtcNow;

            album.LastCompleted = dateUpdated;
            album.DateUpdated = dateUpdated;

            await _context.SaveChangesAsync();
        }
    }
}
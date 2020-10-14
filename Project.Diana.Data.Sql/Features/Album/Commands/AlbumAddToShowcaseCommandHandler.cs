using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumAddToShowcaseCommandHandler : ICommandHandler<AlbumAddToShowcaseCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public AlbumAddToShowcaseCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(AlbumAddToShowcaseCommand command)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(a
                => a.ID == command.AlbumId
                   && a.UserID == command.User.Id);

            if (album is null)
            {
                return;
            }

            if (album.IsShowcased)
            {
                return;
            }

            album.IsShowcased = true;
            album.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
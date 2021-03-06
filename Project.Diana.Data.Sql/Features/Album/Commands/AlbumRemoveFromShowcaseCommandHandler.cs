using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumRemoveFromShowcaseCommandHandler : ICommandHandler<AlbumRemoveFromShowcaseCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public AlbumRemoveFromShowcaseCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(AlbumRemoveFromShowcaseCommand command)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(a
                => a.Id == command.AlbumId
                   && a.UserId == command.User.Id);

            if (album is null)
            {
                return;
            }

            if (!album.IsShowcased)
            {
                return;
            }

            album.IsShowcased = false;
            album.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
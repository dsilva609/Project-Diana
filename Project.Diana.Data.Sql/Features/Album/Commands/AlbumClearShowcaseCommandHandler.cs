using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumClearShowcaseCommandHandler : ICommandHandler<AlbumClearShowcaseCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public AlbumClearShowcaseCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(AlbumClearShowcaseCommand command)
        {
            var albums = await _context.Albums.Where(a =>
                a.IsShowcased
                && a.UserID == command.User.Id).ToListAsync();

            var updateTime = DateTime.UtcNow;

            foreach (var album in albums)
            {
                album.IsShowcased = false;
                album.DateUpdated = updateTime;
            }

            await _context.SaveChangesAsync();
        }
    }
}
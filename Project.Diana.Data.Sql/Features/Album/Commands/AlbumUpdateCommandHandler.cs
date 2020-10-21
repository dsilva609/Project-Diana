using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Album.Commands;
using Project.Diana.Data.Features.Item;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Album.Commands
{
    public class AlbumUpdateCommandHandler : ICommandHandler<AlbumUpdateCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public AlbumUpdateCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(AlbumUpdateCommand command)
        {
            var sameAlbumExists = await _context.Albums.AnyAsync(a =>
                a.ID != command.AlbumId
                && a.Artist.ToUpper() == command.Artist.ToUpper()
                && a.MediaType == command.MediaType
                && a.Title.ToUpper() == command.Title.ToUpper()
                && a.UserID.ToUpper() == command.User.Id.ToUpper());

            if (sameAlbumExists)
            {
                return;
            }

            var existingAlbum = await _context.Albums.FirstOrDefaultAsync(a =>
                a.ID == command.AlbumId
                && a.UserID.ToUpper() == command.User.Id.ToUpper());

            if (existingAlbum is null)
            {
                return;
            }

            var dateUpdated = DateTime.UtcNow;

            existingAlbum.Artist = command.Artist;
            existingAlbum.Category = command.Category;
            existingAlbum.CompletionStatus = command.CompletionStatus;
            existingAlbum.CountryOfOrigin = command.CountryOfOrigin;
            existingAlbum.CountryPurchased = command.CountryPurchased;
            existingAlbum.DatePurchased = command.DatePurchased;
            existingAlbum.DiscogsID = command.DiscogsId;
            existingAlbum.Genre = command.Genre;
            existingAlbum.ImageUrl = command.ImageUrl;
            existingAlbum.IsNew = command.IsNew;
            existingAlbum.IsPhysical = command.IsPhysical;
            existingAlbum.LocationPurchased = command.LocationPurchased;
            existingAlbum.MediaType = command.MediaType;
            existingAlbum.Notes = command.Notes;
            existingAlbum.TimesCompleted = command.TimesCompleted;
            existingAlbum.RecordLabel = command.RecordLabel;
            existingAlbum.Size = command.Size;
            existingAlbum.Speed = command.Speed;
            existingAlbum.Style = command.Style;
            existingAlbum.Title = command.Title;
            existingAlbum.YearReleased = command.YearReleased;

            if (existingAlbum.TimesCompleted > 0 && existingAlbum.CompletionStatus != CompletionStatusReference.Completed)
            {
                existingAlbum.CompletionStatus = CompletionStatusReference.Completed;
                existingAlbum.LastCompleted = dateUpdated;
            }

            if (existingAlbum.CompletionStatus == CompletionStatusReference.Completed && existingAlbum.TimesCompleted == 0)
            {
                existingAlbum.TimesCompleted = 1;
                existingAlbum.LastCompleted = dateUpdated;
            }

            existingAlbum.DateUpdated = dateUpdated;

            await _context.SaveChangesAsync();
        }
    }
}
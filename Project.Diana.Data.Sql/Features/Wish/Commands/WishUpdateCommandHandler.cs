using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Commands
{
    public class WishUpdateCommandHandler : ICommandHandler<WishUpdateCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public WishUpdateCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(WishUpdateCommand command)
        {
            var existingRecord = await _context.Wishes.FirstOrDefaultAsync(w => w.Id == command.WishId && w.UserId == command.UserId);

            if (existingRecord is null)
            {
                throw new InvalidOperationException("Unable to find wish record for update");
            }

            existingRecord.ApiId = command.ApiId;
            existingRecord.Category = command.Category;
            existingRecord.ImageUrl = command.ImageUrl;
            existingRecord.ItemType = command.ItemType;
            existingRecord.Notes = command.Notes;
            existingRecord.Owned = command.Owned;
            existingRecord.Title = command.Title;

            existingRecord.DateModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
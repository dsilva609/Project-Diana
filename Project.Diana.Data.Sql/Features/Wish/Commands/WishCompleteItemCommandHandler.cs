using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Commands
{
    public class WishCompleteItemCommandHandler : ICommandHandler<WishCompleteItemCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public WishCompleteItemCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(WishCompleteItemCommand command)
        {
            var existingRecord = await _context.Wishes.FirstOrDefaultAsync(w => w.Id == command.WishId && w.UserId == command.UserId);

            if (existingRecord is null)
            {
                throw new InvalidOperationException("Unable to find wish record for user");
            }

            existingRecord.Owned = true;
            existingRecord.DateModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
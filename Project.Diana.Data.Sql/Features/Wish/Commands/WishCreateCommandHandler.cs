using System;
using System.Threading.Tasks;
using Project.Diana.Data.Features.Wish;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Commands
{
    public class WishCreateCommandHandler : ICommandHandler<WishCreateCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public WishCreateCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(WishCreateCommand command)
        {
            var createdDate = DateTime.UtcNow;

            var newWish = new WishRecord
            {
                ApiID = command.ApiID,
                Category = command.Category,
                DateAdded = createdDate,
                DateModified = createdDate,
                ImageUrl = command.ImageUrl,
                ItemType = command.ItemType,
                Notes = command.Notes,
                Title = command.Title,
                UserID = command.UserID
            };

            await _context.Wishes.AddAsync(newWish);

            await _context.SaveChangesAsync();
        }
    }
}
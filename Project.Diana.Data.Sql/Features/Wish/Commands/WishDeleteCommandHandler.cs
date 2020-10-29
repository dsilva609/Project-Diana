using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.Wish.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.Wish.Commands
{
    public class WishDeleteCommandHandler : ICommandHandler<WishDeleteCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public WishDeleteCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(WishDeleteCommand command)
        {
            var wish = await _context.Wishes.FirstOrDefaultAsync(w
                => w.ID == command.Id
                && w.UserID == command.User.Id);

            if (wish is null)
            {
                return;
            }

            _context.Wishes.Remove(wish);

            await _context.SaveChangesAsync();
        }
    }
}
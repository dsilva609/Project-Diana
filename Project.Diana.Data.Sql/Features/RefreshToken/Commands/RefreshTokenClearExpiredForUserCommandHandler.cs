using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.RefreshToken.Commands
{
    public class RefreshTokenClearExpiredForUserCommandHandler : ICommandHandler<RefreshTokenClearExpiredForUserCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public RefreshTokenClearExpiredForUserCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(RefreshTokenClearExpiredForUserCommand command)
        {
            var refreshTokens = await _context
                .RefreshTokens
                .Where(token => token.UserId == command.UserId)
                .ToListAsync();

            var expiredTokens = refreshTokens.Where(token => !token.IsActive).ToList();

            var activeTokenTokenToRemove = refreshTokens.FirstOrDefault(token => token.Token == command.ActiveTokenForExpiration);

            if (activeTokenTokenToRemove != null)
            {
                expiredTokens.Add(activeTokenTokenToRemove);
            }

            _context.RefreshTokens.RemoveRange(expiredTokens);

            await _context.SaveChangesAsync();
        }
    }
}
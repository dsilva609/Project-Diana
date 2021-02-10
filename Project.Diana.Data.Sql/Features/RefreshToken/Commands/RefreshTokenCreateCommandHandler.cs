using System;
using System.Threading.Tasks;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.RefreshTokens.Commands;
using Project.Diana.Data.Sql.Bases.Commands;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.RefreshToken.Commands
{
    public class RefreshTokenCreateCommandHandler : ICommandHandler<RefreshTokenCreateCommand>
    {
        private readonly IProjectDianaWriteContext _context;

        public RefreshTokenCreateCommandHandler(IProjectDianaWriteContext context) => _context = context;

        public async Task Handle(RefreshTokenCreateCommand command)
        {
            var newRefreshToken = new RefreshTokenRecord
            {
                CreatedOn = DateTimeOffset.UtcNow,
                ExpiresOn = command.ExpiresOn,
                Token = command.Token,
                UserId = command.UserId
            };

            await _context.RefreshTokens.AddAsync(newRefreshToken);

            await _context.SaveChangesAsync();
        }
    }
}
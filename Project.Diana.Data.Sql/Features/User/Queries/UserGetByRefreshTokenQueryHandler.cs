using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.User.Queries
{
    public class UserGetByRefreshTokenQueryHandler : IQueryHandler<UserGetByRefreshTokenQuery, Result<ApplicationUser>>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public UserGetByRefreshTokenQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<Result<ApplicationUser>> Handle(UserGetByRefreshTokenQuery query)
        {
            var user = await _context
                .Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(usr => usr.RefreshTokens.Any(token => token.Token == query.RefreshToken));

            return user is null
                ? Result.Failure<ApplicationUser>("Unable to find user")
                : Result.Success(user);
        }
    }
}
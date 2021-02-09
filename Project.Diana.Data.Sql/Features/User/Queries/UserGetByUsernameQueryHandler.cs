using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Queries;
using Project.Diana.Data.Sql.Context;

namespace Project.Diana.Data.Sql.Features.User.Queries
{
    public class UserGetByUsernameQueryHandler : IQueryHandler<UserGetByUsernameQuery, ApplicationUser>
    {
        private readonly IProjectDianaReadonlyContext _context;

        public UserGetByUsernameQueryHandler(IProjectDianaReadonlyContext context) => _context = context;

        public async Task<ApplicationUser> Handle(UserGetByUsernameQuery query)
            => await _context
                .Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(user => user.UserName.ToLower() == query.Username.ToLower());
    }
}
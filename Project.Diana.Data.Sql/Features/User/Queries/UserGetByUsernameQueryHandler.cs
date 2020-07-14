using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Project.Diana.Data.Features.User;
using Project.Diana.Data.Features.User.Queries;
using Project.Diana.Data.Sql.Bases.Queries;

namespace Project.Diana.Data.Sql.Features.User.Queries
{
    public class UserGetByUsernameQueryHandler : IQueryHandler<UserGetByUsernameQuery, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGetByUsernameQueryHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<ApplicationUser> Handle(UserGetByUsernameQuery query) => await _userManager.FindByEmailAsync(query.Username);
    }
}
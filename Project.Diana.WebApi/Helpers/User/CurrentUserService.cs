using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Helpers.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var currentUser = _httpContextAccessor?.HttpContext?.User;

            return await _userManager.GetUserAsync(currentUser);
        }
    }
}
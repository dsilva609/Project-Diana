using System.Threading.Tasks;
using Project.Diana.Data.Features.User;

namespace Project.Diana.WebApi.Helpers
{
    public interface ICurrentUserService
    {
        Task<ApplicationUser> GetCurrentUser();
    }
}
using Microsoft.AspNetCore.Identity;

namespace Project.Diana.Data.Features.User
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public bool EnableImport { get; set; }
        public int UserNum { get; set; }
    }
}
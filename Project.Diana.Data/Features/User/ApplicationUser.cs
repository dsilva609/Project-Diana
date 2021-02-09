using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Project.Diana.Data.Features.RefreshTokens;

namespace Project.Diana.Data.Features.User
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public bool EnableImport { get; set; }
        public ICollection<RefreshTokenRecord> RefreshTokens { get; set; }
        public int UserNum { get; set; }
    }
}
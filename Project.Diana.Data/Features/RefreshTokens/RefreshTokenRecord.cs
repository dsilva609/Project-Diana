using System;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Features.RefreshTokens
{
    public class RefreshTokenRecord
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public Guid Id { get; set; }
        public bool IsActive => DateTimeOffset.UtcNow <= ExpiresOn;
        public string Token { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
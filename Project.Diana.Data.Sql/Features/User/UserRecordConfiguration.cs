using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Diana.Data.Features.RefreshTokens;
using Project.Diana.Data.Features.User;

namespace Project.Diana.Data.Sql.Features.User
{
    public class UserRecordConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .ToTable("AspNetUsers")
                .HasKey(key => key.Id);

            builder.HasMany<RefreshTokenRecord>(user => user.RefreshTokens)
                .WithOne(token => token.User)
                .HasPrincipalKey(pKey => pKey.Id)
                .HasForeignKey(key => key.UserId);
        }
    }
}
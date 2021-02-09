using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Diana.Data.Features.RefreshTokens;

namespace Project.Diana.Data.Sql.Features.RefreshToken
{
    public class RefreshRecordConfiguration : IEntityTypeConfiguration<RefreshTokenRecord>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenRecord> builder)
        {
            builder
                .ToTable("RefreshTokens")
                .HasKey(key => key.Id);

            builder.Property(p => p.UserId);
        }
    }
}
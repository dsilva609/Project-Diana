using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Diana.Data.Features.Wish;

namespace Project.Diana.Data.Sql.Features.Wish
{
    public class WishRecordConfiguration : IEntityTypeConfiguration<WishRecord>
    {
        public void Configure(EntityTypeBuilder<WishRecord> builder)
            => builder
                .ToTable("Wishes")
                .HasKey(key => key.Id);
    }
}
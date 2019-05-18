using History.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace History.Infrastructure.EntitiesConfiguration
{
    internal class HistoryEntityTypeConfiguration : IEntityTypeConfiguration<HistoryEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryEntity> builder)
        {
            builder.ToTable("HistoryEntity");
            builder.HasKey(ci => new { ci.UserId, ci.ProfileId });
        }
    }
}
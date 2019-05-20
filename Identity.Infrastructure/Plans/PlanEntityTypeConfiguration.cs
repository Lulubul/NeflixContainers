using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure
{
    internal class PlanEntityTypeConfiguration : IEntityTypeConfiguration<PlanEntity>
    {
        public void Configure(EntityTypeBuilder<PlanEntity> builder)
        {
            builder.ToTable("PlanEntity");
            builder.HasKey(ci => ci.Id );
        }
    }
}
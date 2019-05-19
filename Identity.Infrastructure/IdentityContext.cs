using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class IdentityContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PlanEntity> Plans { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlanEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}

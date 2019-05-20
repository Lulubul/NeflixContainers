using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class UserIdentityContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PlanEntity> Plans { get; set; }

        public UserIdentityContext(DbContextOptions<UserIdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlanEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}

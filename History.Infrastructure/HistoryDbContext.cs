using History.Infrastructure.Entities;
using History.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace History.Infrastructure
{
    public class HistoryDbContext : DbContext
    {
        public DbSet<HistoryEntity> History { get; set; }

        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HistoryEntityTypeConfiguration());
        }
    }
}

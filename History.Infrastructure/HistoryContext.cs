using History.Infrastructure.Entities;
using History.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace History.Infrastructure
{
    public class HistoryContext : DbContext
    {
        public DbSet<HistoryEntity> History { get; set; }

        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HistoryEntityTypeConfiguration());
        }
    }
}

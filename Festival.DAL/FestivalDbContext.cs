using Microsoft.EntityFrameworkCore;

using Festival.DAL.Entities;

namespace Festival.DAL
{
    public class FestivalDbContext : DbContext 
    {
        public FestivalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BandEntity> Bands { get; set; }
        public DbSet<SlotEntity> Slots { get; set; }
        public DbSet<StageEntity> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
using LyricsCollector.Entities;
using Microsoft.EntityFrameworkCore;

namespace LyricsCollector.Context
{
    public class LyricsCollectorDbContext : DbContext
    {
        // List all entities for Entity Framework to "know" them
        public DbSet<Lyrics> Lyrics { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CollectionLyrics> CollectionLyrics { get; set; }

        public LyricsCollectorDbContext(DbContextOptions<LyricsCollectorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.CollectionOfUserId);

            modelBuilder.Entity<CollectionLyrics>().HasKey(cl => new { cl.LyricsId, cl.CollectionId });
        }
    }
}

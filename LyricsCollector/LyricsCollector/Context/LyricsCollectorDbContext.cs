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

        public LyricsCollectorDbContext(DbContextOptions<LyricsCollectorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set relationships using Fluent API

            modelBuilder.Entity<Lyrics>()
                .HasOne<Collection>(l => l.Collection)
                .WithMany(c => c.Lyrics)
                .HasForeignKey(l => l.LyricsOfCollectionId);

            modelBuilder.Entity<Collection>()
                .HasOne<User>(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.CollectionOfUserId);
        }
    }
}

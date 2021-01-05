using LyricsCollector.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LyricsCollector.Context
{
    public class LyricsCollectorDbContext : IdentityDbContext
    {
        // List all entities for Entity Framework to "know" them
        public DbSet<Lyrics> Lyrics { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public override DbSet<IdentityUser> Users { get; set; }

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
        }
    }
}

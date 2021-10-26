using Microsoft.EntityFrameworkCore;
using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Data.Contexts
{
    public class ConduitContext : DbContext
    {
        public ConduitContext(DbContextOptions<ConduitContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Slug);
            });
        }
    }
}

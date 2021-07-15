using Microsoft.EntityFrameworkCore;
using realworlddotnet.Domain.Entities;

namespace realworlddotnet.Infrastructure.Contexts
{
    public class ConduitContext : DbContext
    {
        public ConduitContext(DbContextOptions<ConduitContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
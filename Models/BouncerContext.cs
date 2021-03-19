using Microsoft.EntityFrameworkCore;

namespace bouncer.Models
{
    public class BouncerContext : DbContext
    {
        public BouncerContext(DbContextOptions<BouncerContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Platforms)
                .HasForeignKey(p => p.OwnerId);
        }
    }
}
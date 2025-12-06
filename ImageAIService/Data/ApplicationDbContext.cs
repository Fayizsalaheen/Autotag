using ImageAIService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageAIService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)       
                .WithMany(u => u.Images)    
                .HasForeignKey(i => i.UserId);
            modelBuilder.Entity<User>()
       .HasIndex(u => u.Email)
       .IsUnique();

            modelBuilder.Entity<Tag>()
        .Property(t => t.Confidence)
        .HasPrecision(5, 4); 
        }
    }
}

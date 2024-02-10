using BlogProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Nickname)
                .IsRequired(false)
                .HasMaxLength(32);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .IsRequired(false)
                .HasMaxLength(32);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .IsRequired(false)
                .HasMaxLength(32);

            modelBuilder.Entity<BlogPostModel>()
                .Property(e => e.Id)
                .IsRequired(true);

            modelBuilder.Entity<BlogPostModel>()
                .Property(e => e.Title)
                .IsRequired(true)
                .HasMaxLength(100);

            modelBuilder.Entity<BlogPostModel>()
                .Property(e => e.Content)
                .IsRequired(true);

            modelBuilder.Entity<BlogPostModel>()
                .Property(e => e.CreatedAt)
                .IsRequired(true);

            modelBuilder.Entity<BlogPostModel>()
                .Property(e => e.Tag)
                .IsRequired(true);



        }
        public DbSet<BlogProject.Models.BlogPostModel> BlogPostModel { get; set; } = default!;
    }
}

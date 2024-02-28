//using _253502.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _253502.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        private DbSet<Author> Authors { get; }
        private DbSet<Book> Books { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne<BookInfo>(t => t.InfoData);
        }
    }
}

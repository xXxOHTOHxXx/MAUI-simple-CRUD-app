using Microsoft.EntityFrameworkCore;

namespace _253502.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Author> Authors { get; }
        DbSet<Book> Books { get; }

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

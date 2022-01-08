using DNP_example2.Models;
using Microsoft.EntityFrameworkCore;

namespace DNP_example2.Appropriately
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // name of database
            optionsBuilder.UseSqlite("Data Source = Authors.db");
        }
    }
}

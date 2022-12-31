using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class LibraryDbContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<BookDataModel> Books { get; set; } = null!;

        public LibraryDbContext()
        {
            // To add connections tring for test
            _connectionString = "data source=LAPTOP-AOP88UU4\\SQLEXPRESS;initial catalog=LibraryDB;User ID=sa;Password=sa123;multipleactiveresultsets=True;";
        }
        public LibraryDbContext(string connectionString)
        {
            _connectionString = connectionString!;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDataModel>(t =>
            {
                t.HasKey(m => m.Id);
                t.Property(m => m.ResourceId).IsRequired(true);
                t.Property(m => m.Id).UseSqlServerIdentityColumn();
                t.Property(m => m.Name).IsRequired(true).HasMaxLength(50);
                t.Property(m => m.AuthorName).IsRequired(true).HasMaxLength(50);
            });
        }
    }
}

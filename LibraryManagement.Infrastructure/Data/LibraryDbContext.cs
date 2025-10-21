using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book -> Author relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Book -> Category relationship
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // BorrowRecord -> Book relationship
        modelBuilder.Entity<BorrowRecord>()
            .HasOne(br => br.Book)
            .WithMany(b => b.BorrowRecords)
            .HasForeignKey(br => br.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        // BorrowRecord -> User relationship
        modelBuilder.Entity<BorrowRecord>()
            .HasOne(br => br.User)
            .WithMany(u => u.BorrowRecords)
            .HasForeignKey(br => br.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
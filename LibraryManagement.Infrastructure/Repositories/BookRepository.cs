using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
    {
        return await _dbSet.Where(b => b.IsAvailable).Include(b => b.Author).Include(b => b.Category).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
    {
        return await _dbSet.Where(b => b.AuthorId == authorId).Include(b => b.Category).ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _dbSet.Where(b => b.CategoryId == categoryId).Include(b => b.Author).ToListAsync();
    }

    public async Task<Book?> GetBookWithDetailsAsync(int id)
    {
        return await _dbSet.Include(b => b.Author).Include(b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
    {
        return await _dbSet.Where(b => b.Title.Contains(searchTerm) || b.ISBN.Contains(searchTerm))
            .Include(b => b.Author).Include(b => b.Category).ToListAsync();
    }

    public async Task<bool> IsBookAvailableAsync(int bookId)
    {
        var book = await _dbSet.FindAsync(bookId);
        return book?.IsAvailable ?? false;
    }

    public async Task<Book?> GetBookWithBorrowRecordsAsync(int id)
    {
        return await _dbSet.Include(b => b.BorrowRecords).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task UpdateBookAvailabilityAsync(int bookId, bool isAvailable)
    {
        var book = await _dbSet.FindAsync(bookId);
        if (book != null)
        {
            book.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
        }
    }
}
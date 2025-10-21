using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class BorrowRecordRepository : Repository<BorrowRecord>, IBorrowRecordRepository
{
    public BorrowRecordRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync()
    {
        return await _dbSet.Where(br => !br.IsReturned).Include(br => br.Book).Include(br => br.User).ToListAsync();
    }

    public async Task<IEnumerable<BorrowRecord>> GetBorrowsByUserAsync(int userId)
    {
        return await _dbSet.Where(br => br.UserId == userId).Include(br => br.Book).ToListAsync();
    }

    public async Task<IEnumerable<BorrowRecord>> GetBorrowsByBookAsync(int bookId)
    {
        return await _dbSet.Where(br => br.BookId == bookId).Include(br => br.User).ToListAsync();
    }

    public async Task<BorrowRecord?> GetBorrowWithDetailsAsync(int id)
    {
        return await _dbSet.Include(br => br.Book).Include(br => br.User).FirstOrDefaultAsync(br => br.Id == id);
    }

    public async Task<IEnumerable<BorrowRecord>> GetOverdueBorrowsAsync()
    {
        var overdueDate = DateTime.Now.AddDays(-14);
        return await _dbSet.Where(br => !br.IsReturned && br.BorrowDate < overdueDate)
            .Include(br => br.Book).Include(br => br.User).ToListAsync();
    }

    public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsByUserAsync(int userId)
    {
        return await _dbSet.Where(br => br.UserId == userId && !br.IsReturned)
            .Include(br => br.Book).ToListAsync();
    }
}
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IBorrowRecordRepository : IRepository<BorrowRecord>
{
    Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync();
    Task<IEnumerable<BorrowRecord>> GetBorrowsByUserAsync(int userId);
    Task<IEnumerable<BorrowRecord>> GetBorrowsByBookAsync(int bookId);
    Task<BorrowRecord?> GetBorrowWithDetailsAsync(int id);
    Task<IEnumerable<BorrowRecord>> GetOverdueBorrowsAsync();
    Task<IEnumerable<BorrowRecord>> GetActiveBorrowsByUserAsync(int userId);
}
using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface IBorrowRecordService
{
    Task<IEnumerable<BorrowRecordDto>> GetAllBorrowRecordsAsync();
    Task<BorrowRecordDto?> GetBorrowRecordByIdAsync(int id);
    Task<BorrowRecordDto> CreateBorrowRecordAsync(BorrowRecordDto borrowRecordDto);
    Task<BorrowRecordDto> UpdateBorrowRecordAsync(BorrowRecordDto borrowRecordDto);
    Task<bool> DeleteBorrowRecordAsync(int id);
    
    // Business logic
    Task<IEnumerable<BorrowRecordDto>> GetActiveBorrowsAsync();
    Task<IEnumerable<BorrowRecordDto>> GetOverdueBorrowsAsync();
    Task<IEnumerable<BorrowRecordDto>> GetBorrowsByUserAsync(int userId);
    Task<IEnumerable<BorrowRecordDto>> GetBorrowsByBookAsync(int bookId);
    Task<bool> ReturnBookAsync(int borrowRecordId);
    Task<bool> CanBorrowBookAsync(int bookId, int userId);
}
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class BorrowRecordService : IBorrowRecordService
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IBookRepository _bookRepository;

    public BorrowRecordService(IBorrowRecordRepository borrowRecordRepository, IBookRepository bookRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BorrowRecordDto>> GetAllBorrowRecordsAsync()
    {
        var borrowRecords = await _borrowRecordRepository.GetAllAsync();
        return borrowRecords.Select(MapToDto);
    }

    public async Task<BorrowRecordDto?> GetBorrowRecordByIdAsync(int id)
    {
        var borrowRecord = await _borrowRecordRepository.GetByIdAsync(id);
        return borrowRecord == null ? null : MapToDto(borrowRecord);
    }

    public async Task<BorrowRecordDto> CreateBorrowRecordAsync(BorrowRecordDto borrowRecordDto)
    {
        var borrowRecord = MapToEntity(borrowRecordDto);
        borrowRecord.BorrowDate = DateTime.Now;
        borrowRecord.IsReturned = false;

        var createdBorrowRecord = await _borrowRecordRepository.AddAsync(borrowRecord);
        
        // Update book availability
        await _bookRepository.UpdateBookAvailabilityAsync(borrowRecord.BookId, false);
        
        return MapToDto(createdBorrowRecord);
    }

    public async Task<BorrowRecordDto> UpdateBorrowRecordAsync(BorrowRecordDto borrowRecordDto)
    {
        await _borrowRecordRepository.UpdateAsync(MapToEntity(borrowRecordDto));
        var updatedBorrowRecord = await _borrowRecordRepository.GetByIdAsync(borrowRecordDto.Id);
        return MapToDto(updatedBorrowRecord!);
    }

    public async Task<bool> DeleteBorrowRecordAsync(int id)
    {
        await _borrowRecordRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<BorrowRecordDto>> GetActiveBorrowsAsync()
    {
        var borrowRecords = await _borrowRecordRepository.GetActiveBorrowsAsync();
        return borrowRecords.Select(MapToDto);
    }

    public async Task<IEnumerable<BorrowRecordDto>> GetOverdueBorrowsAsync()
    {
        var borrowRecords = await _borrowRecordRepository.GetOverdueBorrowsAsync();
        return borrowRecords.Select(MapToDto);
    }

    public async Task<IEnumerable<BorrowRecordDto>> GetBorrowsByUserAsync(int userId)
    {
        var borrowRecords = await _borrowRecordRepository.GetBorrowsByUserAsync(userId);
        return borrowRecords.Select(MapToDto);
    }

    public async Task<IEnumerable<BorrowRecordDto>> GetBorrowsByBookAsync(int bookId)
    {
        var borrowRecords = await _borrowRecordRepository.GetBorrowsByBookAsync(bookId);
        return borrowRecords.Select(MapToDto);
    }

    public async Task<bool> ReturnBookAsync(int borrowRecordId)
    {
        var borrowRecord = await _borrowRecordRepository.GetByIdAsync(borrowRecordId);
        if (borrowRecord == null || borrowRecord.IsReturned)
            return false;

        borrowRecord.ReturnDate = DateTime.Now;
        borrowRecord.IsReturned = true;

        await _borrowRecordRepository.UpdateAsync(borrowRecord);
        await _bookRepository.UpdateBookAvailabilityAsync(borrowRecord.BookId, true);

        return true;
    }

    public async Task<bool> CanBorrowBookAsync(int bookId, int userId)
    {
        var isBookAvailable = await _bookRepository.IsBookAvailableAsync(bookId);
        if (!isBookAvailable) return false;

        var activeBorrows = await _borrowRecordRepository.GetActiveBorrowsByUserAsync(userId);
        return activeBorrows.Count() < 3; // Max 3 books per user
    }

    private static BorrowRecordDto MapToDto(BorrowRecord borrowRecord)
    {
        var daysBorrowed = borrowRecord.ReturnDate.HasValue 
            ? (borrowRecord.ReturnDate.Value - borrowRecord.BorrowDate).Days
            : (DateTime.Now - borrowRecord.BorrowDate).Days;

        return new BorrowRecordDto
        {
            Id = borrowRecord.Id,
            BorrowDate = borrowRecord.BorrowDate,
            ReturnDate = borrowRecord.ReturnDate,
            IsReturned = borrowRecord.IsReturned,
            BookId = borrowRecord.BookId,
            UserId = borrowRecord.UserId,
            BookTitle = borrowRecord.Book?.Title ?? string.Empty,
            UserName = $"{borrowRecord.User?.FirstName} {borrowRecord.User?.LastName}".Trim(),
            DaysBorrowed = daysBorrowed,
            IsOverdue = !borrowRecord.IsReturned && daysBorrowed > 14
        };
    }

    private static BorrowRecord MapToEntity(BorrowRecordDto borrowRecordDto)
    {
        return new BorrowRecord
        {
            Id = borrowRecordDto.Id,
            BorrowDate = borrowRecordDto.BorrowDate,
            ReturnDate = borrowRecordDto.ReturnDate,
            IsReturned = borrowRecordDto.IsReturned,
            BookId = borrowRecordDto.BookId,
            UserId = borrowRecordDto.UserId
        };
    }
}
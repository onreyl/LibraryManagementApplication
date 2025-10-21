using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetAvailableBooksAsync();
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);
    Task<Book?> GetBookWithDetailsAsync(int id);
    Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
    Task<bool> IsBookAvailableAsync(int bookId);
    Task<Book?> GetBookWithBorrowRecordsAsync(int id);
    Task UpdateBookAvailabilityAsync(int bookId, bool isAvailable);
}
using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(BookDto bookDto);
    Task<BookDto?> UpdateBookAsync(int id, BookDto bookDto);
    Task<bool> DeleteBookAsync(int id);
    
    // Book-specific business logic
    Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
    Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId);
    Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId);
    Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
    Task<bool> IsBookAvailableAsync(int bookId);
    Task<bool> CanDeleteBookAsync(int id); 
}

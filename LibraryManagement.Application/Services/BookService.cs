using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Select(MapToDto);
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        return book == null ? null : MapToDto(book);
    }

    public async Task<BookDto> CreateBookAsync(BookDto bookDto)
    {
        var book = MapToEntity(bookDto);
        var createdBook = await _bookRepository.AddAsync(book);
        return MapToDto(createdBook);
    }

    public async Task<BookDto?> UpdateBookAsync(int id, BookDto bookDto)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
            return null;
        
        bookDto.Id = id;
        await _bookRepository.UpdateAsync(MapToEntity(bookDto));
        var updatedBook = await _bookRepository.GetByIdAsync(id);
        return MapToDto(updatedBook!);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        if (!await CanDeleteBookAsync(id))
            return false;

        await _bookRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
    {
        var books = await _bookRepository.GetAvailableBooksAsync();
        return books.Select(MapToDto);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId)
    {
        var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
        return books.Select(MapToDto);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
    {
        var books = await _bookRepository.GetBooksByCategoryAsync(categoryId);
        return books.Select(MapToDto);
    }

    public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
    {
        var books = await _bookRepository.SearchBooksAsync(searchTerm);
        return books.Select(MapToDto);
    }

    public async Task<bool> IsBookAvailableAsync(int bookId)
    {
        return await _bookRepository.IsBookAvailableAsync(bookId);
    }

    public async Task<bool> CanDeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetBookWithBorrowRecordsAsync(id);
        return book?.BorrowRecords?.Any(br => !br.IsReturned) ?? true;
    }

    private BookDto MapToDto(Book book)
    {
        return _mapper.Map<BookDto>(book);
    }

    private Book MapToEntity(BookDto bookDto)
    {
        return _mapper.Map<Book>(bookDto);
    }
}
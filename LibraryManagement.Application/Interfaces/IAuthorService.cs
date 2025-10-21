using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
    Task<AuthorDto?> UpdateAuthorAsync(AuthorDto authorDto);
    Task<bool> DeleteAuthorAsync(int id);
    Task<bool> CanDeleteAuthorAsync(int id); // Business rule
    Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync();
}

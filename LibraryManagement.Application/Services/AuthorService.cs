using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<bool> CanDeleteAuthorAsync(int id)
    {
        var author = await _authorRepository.GetAuthorWithBooksAsync(id);
        return author?.Books?.Count == 0;
    }

    public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
    {
        var author = MapToEntity(authorDto);
        var createdAuthor = await _authorRepository.AddAsync(author);
        return MapToDto(createdAuthor, false);
    }


    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _authorRepository.GetAuthorWithBooksAsync(id);

        if((author?.Books?.Count) > 0)
        {
            return false;
        }

        await _authorRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(author => MapToDto(author, false));
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        return author == null ? null : MapToDto(author, false);
    }

    public async Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync()
    {
        var authors = await _authorRepository.GetAuthorsWithBooksAsync();
        return authors.Select(author => MapToDto(author, true));
    }

    public async Task<AuthorDto?> UpdateAuthorAsync(AuthorDto authorDto)
    {
        await _authorRepository.UpdateAsync(MapToEntity(authorDto));
        var updatedAuthor = await _authorRepository.GetByIdAsync(authorDto.Id);
        return updatedAuthor == null ? null : MapToDto(updatedAuthor, false);
    }

    private static AuthorDto MapToDto(Author author, bool includeBookCount)
    {
        return new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography,
            BirthDate = author.BirthDate,
            BookCount = includeBookCount ? (author.Books?.Count ?? 0) : 0
        }; 
    }

    private static Author MapToEntity(AuthorDto authorDto)
    {
        return new Author
        {
            Id = authorDto.Id,
            FirstName = authorDto.FirstName,
            LastName = authorDto.LastName,
            Biography = authorDto.Biography,
            BirthDate = authorDto.BirthDate
        };
    }

}

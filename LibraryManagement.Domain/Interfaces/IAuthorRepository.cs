using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
    Task<Author?> GetAuthorWithBooksAsync(int id);
}
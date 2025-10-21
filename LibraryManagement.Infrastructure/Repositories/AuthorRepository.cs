using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
    {
        return await _dbSet.Include(a => a.Books).ToListAsync();
    }

    public async Task<Author?> GetAuthorWithBooksAsync(int id)
    {
        return await _dbSet.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
    }
}
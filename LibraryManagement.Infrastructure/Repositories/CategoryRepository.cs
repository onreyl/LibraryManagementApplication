using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<Category>> GetCategoriesWithBooksAsync()
    {
        return await _dbSet.Include(c => c.Books).ToListAsync();
    }

    public async Task<Category?> GetCategoryWithBooksAsync(int id)
    {
        return await _dbSet.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
    }
}
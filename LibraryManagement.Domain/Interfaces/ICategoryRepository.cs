using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesWithBooksAsync();
    Task<Category?> GetCategoryWithBooksAsync(int id);
}
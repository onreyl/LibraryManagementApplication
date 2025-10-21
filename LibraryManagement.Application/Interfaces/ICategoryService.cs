using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
    Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);
    Task<bool> DeleteCategoryAsync(int id);
    Task<bool> CanDeleteCategoryAsync(int id); // Business rule
    Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync();
}

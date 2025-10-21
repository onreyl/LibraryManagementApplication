using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(category => MapToDto(category, false));
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category == null ? null : MapToDto(category, false);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
    {
        var category = MapToEntity(categoryDto);
        var createdCategory = await _categoryRepository.AddAsync(category);
        return MapToDto(createdCategory, false);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto)
    {
        await _categoryRepository.UpdateAsync(MapToEntity(categoryDto));
        var updatedCategory = await _categoryRepository.GetByIdAsync(categoryDto.Id);
        return MapToDto(updatedCategory!, false);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        if (!await CanDeleteCategoryAsync(id))
            return false;

        await _categoryRepository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> CanDeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryWithBooksAsync(id);
        return category?.Books?.Count == 0;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesWithBooksAsync()
    {
        var categories = await _categoryRepository.GetCategoriesWithBooksAsync();
        return categories.Select(category => MapToDto(category, true));
    }

    private static CategoryDto MapToDto(Category category, bool includeBookCount)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            BookCount = includeBookCount ? (category.Books?.Count ?? 0) : 0
        };
    }

    private static Category MapToEntity(CategoryDto categoryDto)
    {
        return new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };
    }
}
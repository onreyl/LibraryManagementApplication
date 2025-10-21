using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int id);

    // User-specific operations
    Task<IEnumerable<UserDto>> GetActiveUsersAsync();
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<bool> CanDeleteUserAsync(int id); // Business rule
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
}

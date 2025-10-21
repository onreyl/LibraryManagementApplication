using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToDto);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var user = MapToEntity(userDto);
        var createdUser = await _userRepository.AddAsync(user);
        return MapToDto(createdUser);
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto)
    {
        await _userRepository.UpdateAsync(MapToEntity(userDto));
        var updatedUser = await _userRepository.GetByIdAsync(userDto.Id);
        return MapToDto(updatedUser!);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        if (!await CanDeleteUserAsync(id))
            return false;

        await _userRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
    {
        var users = await _userRepository.GetActiveUsersAsync();
        return users.Select(MapToDto);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user == null ? null : MapToDto(user);
    }

    public async Task<bool> CanDeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserWithBorrowRecordsAsync(id);
        return user?.BorrowRecords?.Any(br => !br.IsReturned) != true;
    }

    public async Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null)
    {
        return await _userRepository.IsEmailUniqueAsync(email, excludeUserId);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsActive = user.IsActive
        };
    }

    private static User MapToEntity(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            IsActive = userDto.IsActive
        };
    }
}
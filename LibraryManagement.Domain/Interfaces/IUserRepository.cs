using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<User?> GetUserWithBorrowRecordsAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null);
}
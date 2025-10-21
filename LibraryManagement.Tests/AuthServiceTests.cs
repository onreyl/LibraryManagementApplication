using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LibraryManagement.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        // Create mock objects
        _mockUserRepository = new Mock<IUserRepository>();
        _mockConfiguration = new Mock<IConfiguration>();

        // Mock JWT settings
        _mockConfiguration.Setup(x => x["JwtSettings:SecretKey"]).Returns("LibraryManagementSuperSecretKey123456789!");
        _mockConfiguration.Setup(x => x["JwtSettings:Issuer"]).Returns("LibraryManagementAPI");
        _mockConfiguration.Setup(x => x["JwtSettings:Audience"]).Returns("LibraryManagementClient");
        _mockConfiguration.Setup(x => x["JwtSettings:ExpirationMinutes"]).Returns("60");

        // Create AuthService with mocked dependencies
        _authService = new AuthService(_mockUserRepository.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var testUser = new User
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "User",
            IsActive = true
        };

        // Setup mock repository to return test user
        _mockUserRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<User> { testUser });

        var loginRequest = new LoginRequestDto
        {
            Email = "test@example.com",
            Password = "123456"
        };

        // Act
        var result = await _authService.LoginAsync(loginRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal("User", result.Role);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ThrowsException()
    {
        // Arrange
        var testUser = new User
        {
            Id = 1,
            Email = "test@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "User"
        };

        _mockUserRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<User> { testUser });

        var loginRequest = new LoginRequestDto
        {
            Email = "test@example.com",
            Password = "wrongpassword" // Invalid password
        };

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            async () => await _authService.LoginAsync(loginRequest)
        );
    }

    [Fact]
    public async Task LoginAsync_NonExistentEmail_ThrowsException()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<User>()); // Empty list

        var loginRequest = new LoginRequestDto
        {
            Email = "yok@example.com",
            Password = "123456"
        };

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            async () => await _authService.LoginAsync(loginRequest)
        );
    }
}

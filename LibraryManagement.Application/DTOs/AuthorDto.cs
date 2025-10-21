using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;
        
        public string FullName => $"{FirstName} {LastName}";
        
        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters")]
        public string? Biography { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public int BookCount { get; set; }
    }
}

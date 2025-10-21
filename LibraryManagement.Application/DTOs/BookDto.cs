using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10-13 characters")]
    public string ISBN { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Range(1000, 2100, ErrorMessage = "Publication year must be between 1000 and 2100")]
    public int PublicationYear { get; set; }
    
    public bool IsAvailable { get; set; }
    
    // Foreign key
    [Required(ErrorMessage = "Author is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Author ID")]
    public int AuthorId { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Category ID")]
    public int CategoryId { get; set; }
    
    // User-friendly (read-only, no validation needed)
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}

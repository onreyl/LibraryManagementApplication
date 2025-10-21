using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Application.DTOs;

public class BorrowRecordDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Borrow date is required")]
    public DateTime BorrowDate { get; set; }
    
    public DateTime? ReturnDate { get; set; }
    
    public bool IsReturned { get; set; }

    // Foreign keys
    [Required(ErrorMessage = "Book is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Book ID")]
    public int BookId { get; set; }
    
    [Required(ErrorMessage = "User is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid User ID")]
    public int UserId { get; set; }

    // User-friendly (read-only, no validation needed)
    public string BookTitle { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    // Business logic (read-only, no validation needed)
    public int DaysBorrowed { get; set; }
    public bool IsOverdue { get; set; } 
}

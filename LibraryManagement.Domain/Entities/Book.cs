namespace LibraryManagement.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public bool IsAvailable { get; set; } = true;


    // Foreign key
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }

    // Navigation property
    public Author Author { get; set; } = null!;
    public Category Category { get; set; } = null!;
    
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}".Trim()))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name ?? string.Empty));

            CreateMap<BookDto, Book>();

            // Author mappings
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.BookCount,
                    opt => opt.MapFrom(src => src.Books.Count));

            CreateMap<AuthorDto, Author>();

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.BookCount,
                    opt => opt.MapFrom(src => src.Books.Count));

            CreateMap<CategoryDto, Category>();

            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ActiveBorrowCount,
                    opt => opt.MapFrom(src => src.BorrowRecords.Count(br => !br.IsReturned)));

            CreateMap<UserDto, User>();

            // BorrowRecord mappings
            CreateMap<BorrowRecord, BorrowRecordDto>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}".Trim()))
                .ForMember(dest => dest.DaysBorrowed,
                    opt => opt.MapFrom(src => (DateTime.Now - src.BorrowDate).Days))
                .ForMember(dest => dest.IsOverdue,
                    opt => opt.MapFrom(src => !src.IsReturned && (DateTime.Now - src.BorrowDate).Days > 14));

            CreateMap<BorrowRecordDto, BorrowRecord>();
        }
    }
}

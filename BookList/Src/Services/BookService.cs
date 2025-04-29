using BookList.Domain;
using BookList.Application.Dtos;
using BookList.Repository.Interfaces;
using BookList.Services.Interfaces;

namespace BookList.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepo _bookRepo;

    public BookService(IBookRepo bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task<Book> CreateBookAsync(BookCreateDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Year = dto.Year,
            PageCount = dto.PageCount,
            Blurp = dto.Blurp,
            AuthorId = dto.AuthorId,
            ISBN = dto.ISBN,
            CoverUrl = dto.CoverUrl,
            PublisherId = dto.PublisherId,
            Category = dto.Category
        };

        await _bookRepo.AddBookAsync(book);
        return book;
    }

    
     public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepo.GetBookByIdAsync(id);
    }

    public async Task<List<BookResponseDto>> GetAllBooksAsync()
{
    var books = await _bookRepo.GetAllBooksAsync();

    // Map books to BookResponseDto
    var bookDtos = books.Select(book => new BookResponseDto
    {
        Id = book.Id,
        Title = book.Title,
        Year = book.Year,
        PageCount = book.PageCount,
        Blurp = book.Blurp,
        Category = book.Category,
        CoverUrl = book.CoverUrl,
        ISBN = book.ISBN,
        Author = new AuthorResponseDto
        {
            FirstName = book.Author.FirstName,
            LastName = book.Author.LastName
        },
        Publisher = new PublisherResponseDto
        {
            Name = book.Publisher.Name
        }
    }).ToList();

    return bookDtos;
}
}


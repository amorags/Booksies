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
}


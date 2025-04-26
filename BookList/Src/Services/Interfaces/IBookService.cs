using BookList.Domain;
using BookList.Application.Dtos;

namespace BookList.Services.Interfaces;


    public interface IBookService
    {
        Task<Book> CreateBookAsync(BookCreateDto dto);

        Task<Book?> GetBookByIdAsync(int id);

        Task<List<BookResponseDto>> GetAllBooksAsync();
    }


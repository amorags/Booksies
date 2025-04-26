using BookList.Domain;

namespace BookList.Repository.Interfaces
{
    public interface IBookRepo
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book updatedBook);
        Task DeleteBookAsync(int id);
    }
}
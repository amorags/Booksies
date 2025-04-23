using BookList.Domain;

namespace BookList.Repository.Interfaces;

public interface IAuthorRepo
{
    Task<List<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author updatedAuthor);
    Task DeleteAuthorAsync(int id);
}
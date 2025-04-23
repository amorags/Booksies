using BookList.Domain;
using BookList.Application.Dtos;
using BookList.Repository.Interfaces;
using BookList.Services.Interfaces;

namespace BookList.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepo _authorRepo;

    public AuthorService(IAuthorRepo authorRepo)
    {
        _authorRepo = authorRepo;
    }

    public async Task<Author> CreateAuthorAsync(CreateAuthorDto dto)
    {
        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate
        };

        await _authorRepo.AddAuthorAsync(author);
        return author;
    }

     public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _authorRepo.GetAuthorByIdAsync(id);
    }
}

using BookList.Domain;
using BookList.Application.Dtos;

namespace BookList.Services.Interfaces;

public interface IAuthorService

{
    Task<Author> CreateAuthorAsync(CreateAuthorDto dto);
    Task<Author?> GetAuthorByIdAsync(int id);
}
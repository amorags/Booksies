using BookList.Domain;
using BookList.Application.Dtos;

namespace BookList.Services.Interfaces;

public interface IPublisherService

{
    Task<Publisher> CreatePublisherAsync(createPublisherDto dto);
    Task<Publisher?> GetPublisherByIdAsync(int id);
}
using BookList.Domain;
using BookList.Application.Dtos;
using BookList.Repository.Interfaces;
using BookList.Services.Interfaces;

namespace BookList.Application.Services;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepo _publisherRepo;

    public PublisherService(IPublisherRepo publisherRepo)
    {
        _publisherRepo = publisherRepo;
    }

    public async Task<Publisher> CreatePublisherAsync(createPublisherDto dto)
    {
        var publisher = new Publisher
        {
            Name = dto.Name,
            Location = dto.Location,
        };

        await _publisherRepo.AddPublisherAsync(publisher);
        return publisher;
    }

     public async Task<Publisher?> GetPublisherByIdAsync(int id)
    {
        return await _publisherRepo.GetPublisherByIdAsync(id);
    }
}

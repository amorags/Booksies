using BookList.Domain;

namespace BookList.Repository.Interfaces;

public interface IPublisherRepo
{
    Task<List<Publisher>> GetAllPublishersAsync();
    Task<Publisher?> GetPublisherByIdAsync(int id);
    Task AddPublisherAsync(Publisher publisher);
}
using Microsoft.EntityFrameworkCore;
using BookList.Domain;
using BookList.Data;
using BookList.Repository.Interfaces;

public class PublisherRepo : IPublisherRepo
{
    private readonly BookContext _context;

    public PublisherRepo(BookContext context)
    {
        _context = context;
    }

    // Get all publishers
    public async Task<List<Publisher>> GetAllPublishersAsync()
    {
        return await _context.Publishers
            .Include(p => p.Books) // If you want to include the related books for each publisher
            .ToListAsync();
    }

    // Get a single publisher by ID
    public async Task<Publisher?> GetPublisherByIdAsync(int id)
    {
        return await _context.Publishers
            .Include(p => p.Books) // If you want to include the related books for the specific publisher
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Add a new publisher
    public async Task AddPublisherAsync(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
    }

    // Update an existing publisher
    public async Task UpdatePublisherAsync(Publisher updatedPublisher)
    {
        _context.Publishers.Update(updatedPublisher);
        await _context.SaveChangesAsync();
    }

    // Delete a publisher by ID
    public async Task DeletePublisherAsync(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher != null)
        {
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }
    }
}

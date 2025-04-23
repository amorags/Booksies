using Microsoft.EntityFrameworkCore;
using BookList.Repository.Interfaces;
using BookList.Domain;
using BookList.Data;

namespace BookList.Repository;

public class AuthorRepo : IAuthorRepo
{
    private readonly BookContext _context;

    public AuthorRepo(BookContext context)
    {
        _context = context;
    }

    // Get all authors
    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors
            .Include(a => a.Books) // If you want to include the related books for each author
            .ToListAsync();
    }

    // Get a single author by ID
    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        return await _context.Authors
            .Include(a => a.Books) // If you want to include the related books for the specific author
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    // Add a new author
    public async Task AddAuthorAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    // Update an existing author
    public async Task UpdateAuthorAsync(Author updatedAuthor)
    {
        _context.Authors.Update(updatedAuthor);
        await _context.SaveChangesAsync();
    }

    // Delete an author by ID
    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}

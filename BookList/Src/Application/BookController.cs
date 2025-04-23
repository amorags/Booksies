using Microsoft.AspNetCore.Mvc;
using BookList.Domain;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookRepo _bookRepo;

    public BookController(BookRepo bookRepo)
    {
        _bookRepo = bookRepo;
    }

    // Get all books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookRepo.GetAllBooksAsync();
        return Ok(books);
    }

    // Get a single book by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookRepo.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    // Create a new book
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        if (book == null)
        {
            return BadRequest();
        }

        await _bookRepo.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    // Update an existing book
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        if (updatedBook == null || updatedBook.Id != id)
        {
            return BadRequest();
        }

        await _bookRepo.UpdateBookAsync(updatedBook);
        return NoContent();
    }

    // Delete a book
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookRepo.DeleteBookAsync(id);
        return NoContent();
    }
}

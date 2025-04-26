using Microsoft.AspNetCore.Mvc;
using BookList.Domain;
using BookList.Application.Dtos;
using BookList.Services.Interfaces;

namespace BookList.Api.Controllers // <-- or whatever your namespace is
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

    

        // Get a single book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // Create a new book
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var createdBook = await _bookService.CreateBookAsync(dto);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using BookList.Application.Dtos;
using BookList.Domain;
using BookList.Services.Interfaces;


[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // Create a new author
    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Author DTO cannot be null.");
        }

        var author = await _authorService.CreateAuthorAsync(dto);
        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    // You should already have this method to support CreatedAtAction
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }
}

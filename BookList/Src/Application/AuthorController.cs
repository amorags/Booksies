using Microsoft.AspNetCore.Mvc;
using BookList.Application.Dtos;
using BookList.Domain;
using BookList.Services.Interfaces;
using Microsoft.Extensions.Options;


[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly FeatureToggles _features;

    public AuthorController(IOptions<FeatureToggles> feature, IAuthorService authorService)
    {
        _authorService = authorService;
        _features = feature.Value;
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

    [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
        if (!_features.EnableBooksByAuthor)
        {
        return NotFound("This feature is not available yet.");
        }

        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
        {
        return NotFound();
        }

        return Ok(author);
    }
}

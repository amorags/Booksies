using Microsoft.AspNetCore.Mvc;
using BookList.Domain;
using BookList.Application.Dtos;
using BookList.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PublisherController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

   

    // Get a single publisher by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublisherById(int id)
    {
        var publisher = await _publisherService.GetPublisherByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }
        return Ok(publisher);
    }

    // Create a new publisher
    [HttpPost]
    public async Task<IActionResult> AddPublisher([FromBody] createPublisherDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Publisher cannot be null.");
        }

        var publisher = await _publisherService.CreatePublisherAsync(dto);
        return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisher);
    }

}

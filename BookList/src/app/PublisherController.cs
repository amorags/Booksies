using Microsoft.AspNetCore.Mvc;
using BookList.Domain;

[ApiController]
[Route("api/[controller]")]
public class PublisherController : ControllerBase
{
    private readonly PublisherRepo _publisherRepo;

    public PublisherController(PublisherRepo publisherRepo)
    {
        _publisherRepo = publisherRepo;
    }

    // Get all publishers
    [HttpGet]
    public async Task<IActionResult> GetPublishers()
    {
        var publishers = await _publisherRepo.GetAllPublishersAsync();
        return Ok(publishers);
    }

    // Get a single publisher by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublisherById(int id)
    {
        var publisher = await _publisherRepo.GetPublisherByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }
        return Ok(publisher);
    }

    // Create a new publisher
    [HttpPost]
    public async Task<IActionResult> AddPublisher([FromBody] Publisher publisher)
    {
        if (publisher == null)
        {
            return BadRequest("Publisher cannot be null.");
        }

        await _publisherRepo.AddPublisherAsync(publisher);
        return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisher);
    }

    // Update an existing publisher
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePublisher(int id, [FromBody] Publisher updatedPublisher)
    {
        if (updatedPublisher == null || updatedPublisher.Id != id)
        {
            return BadRequest("Publisher ID mismatch.");
        }

        await _publisherRepo.UpdatePublisherAsync(updatedPublisher);
        return NoContent();
    }

    // Delete a publisher
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        await _publisherRepo.DeletePublisherAsync(id);
        return NoContent();
    }
}

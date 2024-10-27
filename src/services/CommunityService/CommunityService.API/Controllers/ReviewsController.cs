using CommunityService.Application.DTOs.Review;
using CommunityService.Application.Reviews.Create;
using CommunityService.Application.Reviews.Delete;
using CommunityService.Application.Reviews.GetById;
using CommunityService.Application.Reviews.GetPaged;
using CommunityService.Application.Reviews.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunityService.API.Controllers;

[Route("/api/reviews")]
public class ReviewsController : BaseApiController
{
    private readonly ISender _sender;
    
    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewRequest request)
    {
        var cmd = new CreateReviewCommand(request);
        var result = await _sender.Send(cmd);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }

        return Created($"/api/reviews/{result.Data}", new {id = result.Data});
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetReviewByIdQuery(id);
        var result = await _sender.Send(query);
        if (!result.Success)
        {
            return NotFound();
        }

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetPagedReviewQuery(pageNumber, pageSize);
        var result = await _sender.Send(query);
        if (!result.Success)
        {
            return BadRequest(result.Data);
        }

        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateReviewRequest request)
    {
        request = request with { ReviewId = id };
        var cmd = new UpdateReviewCommand(request);
        var result = await _sender.Send(cmd);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cmd = new DeleteReviewCommand(id);
        var result = await _sender.Send(cmd);
        if (!result.Success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
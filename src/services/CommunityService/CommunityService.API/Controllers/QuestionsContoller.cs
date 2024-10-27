using CommunityService.Application.DTOs.Question;
using CommunityService.Application.Questions.Create;
using CommunityService.Application.Questions.Delete;
using CommunityService.Application.Questions.GetById;
using CommunityService.Application.Questions.GetPaged;
using CommunityService.Application.Questions.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunityService.API.Controllers;

[Route("/api/questions")]
public class QuestionsContoller : BaseApiController
{
    private readonly ISender _sender;
    
    public QuestionsContoller(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut]
    public async Task<IActionResult> Create(CreateQuestionRequest request)
    {
        var cmd = new CreateQuestionCommand(request);
        var result = await _sender.Send(cmd);
        if (!result.Success)
        {
            return BadRequest();
        }

        return Created($"/api/questions/{result.Data}", new {id=result.Data});
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetQuestionByIdQuery(id);
        var result = await _sender.Send(query);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }

        return Ok(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetPagedQuestionQuery(pageNumber, pageSize);
        var result = await _sender.Send(query);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }
        
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateQuestionRequest request)
    {
        request = request with { Id = id };
        var cmd = new UpdateQuestionCommand(request);
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
        var cmd = new DeleteQuestionCommand(id);
        var result = await _sender.Send(cmd);
        if (!result.Success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
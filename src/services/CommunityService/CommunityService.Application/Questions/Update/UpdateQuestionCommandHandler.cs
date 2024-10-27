using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Questions.Update;

public record UpdateQuestionCommandHandler : ICommandHandler<UpdateQuestionCommand>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<UpdateQuestionRequest> _validator;
    
    public UpdateQuestionCommandHandler(IAppDbContext db, IValidator<UpdateQuestionRequest> validator)
    {
        _db = db;
        _validator = validator;
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ErrorOr<Unit>.BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation Error Occured");
        }

        var question = await _db.Questions.FirstOrDefaultAsync(q => q.Id == QuestionId.Of(request.Request.Id), cancellationToken);
        if (question is null)
        {
            return ErrorOr<Unit>.NotFound();
        }

        question.Text = request.Request.Text;
        question.CustomerId = request.Request.CustomerId;
        question.ProductId = request.Request.ProductId;

        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}
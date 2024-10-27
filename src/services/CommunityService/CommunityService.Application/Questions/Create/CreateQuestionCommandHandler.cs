using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.Models;
using CommunityService.Domain.ValueObjects;
using FluentValidation;

namespace CommunityService.Application.Questions.Create;

public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, ErrorOr<Guid>>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<CreateQuestionRequest> _validator;
    
    public CreateQuestionCommandHandler(IAppDbContext db, IValidator<CreateQuestionRequest> validator)
    {
        _db = db;
        _validator = validator;
    }
    public async Task<ErrorOr<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ErrorOr<Guid>.BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation Error Occured");
        }

        var question = new Question()
        {
            Id = QuestionId.Of(Guid.NewGuid()),
            Text = request.Request.Text,
            CreatedOn = DateTime.UtcNow,
            CustomerId = request.Request.CustomerId,
            ProductId = request.Request.ProductId,
            UpdatedOn = DateTime.UtcNow
        };

        await _db.Questions.AddAsync(question, cancellationToken);
        await _db.SaveChangesAsync();

        return question.Id.Id;
    }
}
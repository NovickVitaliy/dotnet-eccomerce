namespace CommunityService.Domain.ValueObjects;

public record QuestionId
{
    public Guid Id { get; }

    private QuestionId(Guid value) => Id = value;

    public static QuestionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(value));
        }
        
        return new QuestionId(value);
    }
}
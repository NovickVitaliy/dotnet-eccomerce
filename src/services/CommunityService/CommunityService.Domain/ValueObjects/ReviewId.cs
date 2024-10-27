namespace CommunityService.Domain.ValueObjects;

public record ReviewId
{
    public Guid Id { get; }
    private ReviewId(Guid Id) => this.Id = Id;

    public static ReviewId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }
        
        return new ReviewId(value);
    }
}
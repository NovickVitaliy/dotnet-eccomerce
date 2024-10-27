namespace CommunityService.Domain.Base;

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public TId Id { get; set; }
}
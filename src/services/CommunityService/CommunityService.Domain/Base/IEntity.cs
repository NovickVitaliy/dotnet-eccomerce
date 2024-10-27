namespace CommunityService.Domain.Base;

public interface IEntity
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}

public interface IEntity<TId> : IEntity
{
    public TId Id { get; set; }
}
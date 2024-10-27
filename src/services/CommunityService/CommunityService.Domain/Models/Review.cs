using CommunityService.Domain.Base;
using CommunityService.Domain.ValueObjects;

namespace CommunityService.Domain.Models;

public class Review : BaseEntity<ReviewId>
{
    public int Score { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public string Comment { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}
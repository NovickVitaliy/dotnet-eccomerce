using CommunityService.Domain.Base;
using CommunityService.Domain.ValueObjects;

namespace CommunityService.Domain.Models;

public class Question : BaseEntity<QuestionId>
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public string Text { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasketService.Domain.Domain;

public class Basket
{
    [BsonId]
    public ObjectId Id { get; init; }
    
    public int UserId { get; set; }
    public List<BasketItem> BasketItems { get; set; }
    public decimal Price { get; set; }
}
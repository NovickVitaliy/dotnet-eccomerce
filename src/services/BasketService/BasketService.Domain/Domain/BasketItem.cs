using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasketService.Domain.Domain;

public class BasketItem
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    public int Quantity { get; set; }
    public int PricePerUnit { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
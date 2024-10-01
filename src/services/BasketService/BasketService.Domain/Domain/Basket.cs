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

public class BasketItem
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    public int Quantity { get; set; }
    public int PricePerUnit { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
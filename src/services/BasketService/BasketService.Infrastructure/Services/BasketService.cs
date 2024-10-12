using AutoMapper;
using BasketService.Application.Contracts;
using BasketService.Application.DTOs.Basket;
using BasketService.Domain.Domain;
using BasketService.Infrastructure.Persistence;
using Common.ErrorHandling;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BasketService.Infrastructure.Services;

public class BasketService : IBasketService
{
    private readonly MongoDbContext _db;
    private readonly IMapper _mapper;
    private const string BasketCollectionName = "Baskets";
    
    public BasketService(MongoDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<string>> CreateBasketAsync(CreateBasketRequest request)
    {
        var collection = GetBasketCollection();

        var basket = _mapper.Map<Basket>(request);
        
        await collection.InsertOneAsync(basket);

        return basket.UserId.ToString();
    }
    
    public async Task<ErrorOr<bool>> UpdateBasketAsync(UpdateBasketRequest request)
    {
        var collection = GetBasketCollection();
        
        var basket = _mapper.Map<Basket>(request);

        var result = await collection.ReplaceOneAsync(Builders<Basket>.Filter.Eq(x => x.UserId, request.UserId), basket);

        return result.ModifiedCount == 1;
    }
    
    public async Task<ErrorOr<bool>> DeleteBasketAsync(string basketId)
    {
        var collection = GetBasketCollection();

        var result = await collection.DeleteOneAsync(x => x.Id == ObjectId.Parse(basketId));

        return result.DeletedCount == 1;
    }
    
    public async Task<ErrorOr<BasketDto>> GetBasketByUserIdAsync(int userId)
    {
        var collection = GetBasketCollection();

        var basket = await (await collection.FindAsync(x => x.UserId == userId)).SingleOrDefaultAsync();

        if (basket is null)
        {
            return ErrorOr<BasketDto>.NotFound();
        }

        var basketDto = _mapper.Map<BasketDto>(basket);

        return basketDto;
    }

    private IMongoCollection<Basket> GetBasketCollection()
    {
        return _db.GetCollection<Basket>(BasketCollectionName);
    }
}
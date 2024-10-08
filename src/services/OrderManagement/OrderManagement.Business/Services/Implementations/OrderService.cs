using AutoMapper;
using Common.ErrorHandling;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Business.DTOs.Order;
using OrderManagement.Business.DTOs.Shipment;
using OrderManagement.Business.Services.Contracts;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.Domain.Models;

namespace OrderManagement.Business.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;
    private readonly IValidator<CreateOrderRequest> _validator;
    private readonly IMapper _mapper;
    
    public OrderService(AppDbContext db, IValidator<CreateOrderRequest> validator, IMapper mapper)
    {
        _db = db;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<ShipmentDto>> CreateOrderAsync(CreateOrderRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ErrorOr<ShipmentDto>.BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var order = _mapper.Map<Order>(request);
            order.OrderStatus = OrderStatus.Created;
            order.OrderDate = DateTime.Now.AddDays(2);
            await _db.Orders.AddAsync(order);

            order.OrderHistory = new OrderHistory()
            {
                OrderId = order.Id,
                StatusChangedDate = DateTime.Now,
                NewStatus = OrderStatus.Created
            };

            order.Shipment = new Shipment()
            {
                OrderId = order.Id,
                EstimatedArrival = DateTime.Now.AddDays(2),
                ShippingDate = DateTime.Now,
                TrackingNumber = Guid.NewGuid()
            };
            
            await _db.OrderHistories.AddAsync(order.OrderHistory);
            await _db.Shipments.AddAsync(order.Shipment);
            await _db.SaveChangesAsync();

            return _mapper.Map<ShipmentDto>(order.Shipment);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return ErrorOr<ShipmentDto>.InternalServerError();
        }
    }
    public async Task<ErrorOr<bool>> ChangeStatusAsync(ChangeOrderStatusRequest request)
    {
        var order = await _db.Orders
            .Include(x => x.OrderHistory)
            .SingleOrDefaultAsync(x => x.Id == request.OrderId);
        
        if (order is null)
        {
            return ErrorOr<bool>.NotFound();
        }

        if (!OrderStatus.Statuses.Any(x => x.ToLower().Equals(request.NewStatus.ToLower())))
        {
            return ErrorOr<bool>.BadRequest("Invalid Status for order");
        }

        order.OrderStatus = request.NewStatus;
        order.OrderHistory.NewStatus = request.NewStatus;
        order.OrderHistory.StatusChangedDate = DateTime.Now;

        await _db.SaveChangesAsync();
        return true;
    }
}
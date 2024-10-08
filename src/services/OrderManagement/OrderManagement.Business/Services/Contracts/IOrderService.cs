using Common.ErrorHandling;
using OrderManagement.Business.DTOs.Order;
using OrderManagement.Business.DTOs.Shipment;

namespace OrderManagement.Business.Services.Contracts;

public interface IOrderService
{
    Task<ErrorOr<ShipmentDto>> CreateOrderAsync(CreateOrderRequest request);
    Task<ErrorOr<bool>> ChangeStatusAsync(ChangeOrderStatusRequest request);
}
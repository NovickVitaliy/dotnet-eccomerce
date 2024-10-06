using OrderManagement.DataAccess.Repositories.Contracts;

namespace OrderManagement.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    ICustomerRepository CustomerRepository { get; init; }
    IOrderHistoryRepository OrderHistoryRepository { get; init; }
    IOrderItemRepository OrderItemRepository { get; init; }
    IOrderRepository OrderRepository { get; init; }
    IShipmentRepository ShipmentRepository { get; init; }

    Task SaveChangesAsync();
    void SaveChanges();
}
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Contracts;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetDetailedOrder(int orderId, bool trackingEnabled = false);
    Task<IReadOnlyCollection<Order>> GetDetailedOrders(int pageNumber, int pageSize, bool trackingEnabled = false);
}
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Contracts;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetCustomerWithOrders(int customerId, bool trackingEnabled = false);
    Task<IReadOnlyCollection<Order>> GetCustomersOrders(int customerId, bool trackingEnabled = false);
}
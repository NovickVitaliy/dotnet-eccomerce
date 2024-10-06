using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<Customer?> GetCustomerWithOrders(int customerId, bool trackingEnabled = false)
    {
        var query = AppDbContext.Customers
            .Include(x => x.CustomerOrders)
            .ThenInclude(x => x.Order)
            .Where(x => x.Id == customerId);
        return trackingEnabled
            ? await query.SingleOrDefaultAsync()
            : await query.AsNoTracking().SingleOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Order>> GetCustomersOrders(int customerId, bool trackingEnabled = false)
    {
        var query = AppDbContext.Orders
            .Where(x => x.CustomerId == customerId);

        return trackingEnabled
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<Order?> GetDetailedOrder(int orderId, bool trackingEnabled = false)
    {
        var query = AppDbContext.Orders
            .Include(x => x.Shipment)
            .Include(x => x.OrderHistory)
            .Include(x => x.OrderItems)
            .Include(x => x.CustomerOrders)
            .ThenInclude(x => x.Customer)
            .AsSplitQuery();

        return trackingEnabled
            ? await query.SingleOrDefaultAsync()
            : await query.AsNoTracking().SingleOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Order>> GetDetailedOrders(int pageNumber, int pageSize, bool trackingEnabled = false)
    {
        var skip = (pageNumber - 1) * pageSize;
        var query = AppDbContext.Orders
            .Include(x => x.Shipment)
            .Include(x => x.OrderHistory)
            .Include(x => x.OrderItems)
            .Include(x => x.CustomerOrders)
            .ThenInclude(x => x.Customer)
            .Skip(skip)
            .Take(pageSize);

        return trackingEnabled
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }
}
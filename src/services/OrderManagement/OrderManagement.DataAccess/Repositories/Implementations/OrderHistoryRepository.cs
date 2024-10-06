using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public class OrderHistoryRepository : GenericRepository<OrderHistory>, IOrderHistoryRepository
{
    public OrderHistoryRepository(AppDbContext appDbContext) : base(appDbContext)
    { }
}
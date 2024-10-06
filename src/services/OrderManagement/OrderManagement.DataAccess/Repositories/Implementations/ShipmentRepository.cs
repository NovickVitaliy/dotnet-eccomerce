using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
{
    public ShipmentRepository(AppDbContext appDbContext) : base(appDbContext)
    { }
}
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;

namespace OrderManagement.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext, ICustomerRepository customerRepository, IOrderHistoryRepository orderHistoryRepository, IOrderItemRepository orderItemRepository,
        IOrderRepository orderRepository, IShipmentRepository shipmentRepository)
    {
        _appDbContext = appDbContext;
        CustomerRepository = customerRepository;
        OrderHistoryRepository = orderHistoryRepository;
        OrderItemRepository = orderItemRepository;
        OrderRepository = orderRepository;
        ShipmentRepository = shipmentRepository;
    }
    
    public ICustomerRepository CustomerRepository { get; init; }
    public IOrderHistoryRepository OrderHistoryRepository { get; init; }
    public IOrderItemRepository OrderItemRepository { get; init; }
    public IOrderRepository OrderRepository { get; init; }
    public IShipmentRepository ShipmentRepository { get; init; }

    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
    public void SaveChanges()
    {
        _appDbContext.SaveChanges();
    }
}
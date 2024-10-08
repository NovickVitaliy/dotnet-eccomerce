using Common.ErrorHandling;
using OrderManagement.Business.DTOs.Customer;

namespace OrderManagement.Business.Services.Contracts;

public interface ICustomerService
{
    Task<ErrorOr<int>> CreateCustomerAsync(CreateCustomerRequest request);
    Task<ErrorOr<IReadOnlyCollection<CustomerDto>>> GetCustomersAsync(int pageNumber, int pageSize);
    Task<ErrorOr<CustomerDto>> GetCustomerByIdAsync(int customerId);
}
using OrderManagement.Business.DTOs.Order;

namespace OrderManagement.Business.DTOs.Customer;

public record CustomerDto(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
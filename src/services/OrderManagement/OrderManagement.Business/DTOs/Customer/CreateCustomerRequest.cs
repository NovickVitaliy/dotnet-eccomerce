namespace OrderManagement.Business.DTOs.Customer;
    
public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
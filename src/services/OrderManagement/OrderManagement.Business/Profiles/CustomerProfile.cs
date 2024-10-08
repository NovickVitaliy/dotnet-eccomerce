using AutoMapper;
using OrderManagement.Business.DTOs.Customer;
using OrderManagement.Domain.Models;

namespace OrderManagement.Business.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerRequest, Customer>();
        CreateMap<Customer, CustomerDto>();
    }
}
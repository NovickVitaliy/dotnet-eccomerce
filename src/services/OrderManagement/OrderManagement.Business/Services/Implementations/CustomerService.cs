using AutoMapper;
using Common.ErrorHandling;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Business.DTOs.Customer;
using OrderManagement.Business.Services.Contracts;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.Domain.Models;

namespace OrderManagement.Business.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;
    private readonly IValidator<CreateCustomerRequest> _validator;
    private readonly IMapper _mapper;
    
    public CustomerService(AppDbContext db, IValidator<CreateCustomerRequest> validator, IMapper mapper)
    {
        _db = db;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<int>> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ErrorOr<int>.BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        var customer = _mapper.Map<Customer>(request);
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return customer.Id;
    }
    public async Task<ErrorOr<IReadOnlyCollection<CustomerDto>>> GetCustomersAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var customers = await _db.Customers
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var dtos = customers.Select(c => _mapper.Map<CustomerDto>(c)).ToList();
        return ErrorOr<IReadOnlyCollection<CustomerDto>>.Ok(dtos);
    }
    public async Task<ErrorOr<CustomerDto>> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
        if (customer is null)
        {
            return ErrorOr<CustomerDto>.NotFound();
        }

        var dto = _mapper.Map<CustomerDto>(customer);
        return ErrorOr<CustomerDto>.Ok(dto);
    }
}
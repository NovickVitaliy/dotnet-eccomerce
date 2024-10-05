using AutoMapper;
using Common.ErrorHandling;
using ProductInventory.Business.DTOs.Supplier;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    
    public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<int>> CreateSupplierAsync(CreateSupplierRequest request)
    {
        try
        {
            var supplier = _mapper.Map<Supplier>(request);
            var result = await _supplierRepository.CreateSupplierAsync(supplier);
            return ErrorOr<int>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<int>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<IReadOnlyList<SupplierDto>>> GetSuppliersAsync(GetSuppliersRequest reqeust)
    {
        try
        {
            var result = await _supplierRepository.GetSuppliersAsync(reqeust.PageNumber, reqeust.PageSize);
            var response = result.Select(s => _mapper.Map<SupplierDto>(s)).ToList().AsReadOnly();
            return ErrorOr<IReadOnlyList<SupplierDto>>.Ok(response);
        }
        catch (Exception e)
        {
            return ErrorOr<IReadOnlyList<SupplierDto>>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<SupplierDto>> GetSupplierByIdAsync(GetSupplierByIdRequest request)
    {
        try
        {
            var result = await _supplierRepository.GetSupplierByIdAsync(request.Id);
            if (result is null)
            {
                return ErrorOr<SupplierDto>.NotFound();
            }
            
            return ErrorOr<SupplierDto>.Ok(_mapper.Map<SupplierDto>(result));
        }
        catch (Exception e)
        {
            return ErrorOr<SupplierDto>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<bool>> DeleteSupplierAsync(DeleteSupplierRequest request)
    {
        try
        {
            var result = await _supplierRepository.DeleteSupplierAsync(request.Id);
            if (!result)
            {
                return ErrorOr<bool>.NotFound();
            }
            
            return ErrorOr<bool>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<bool>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<bool>> UpdateSupplierAsync(UpdateSupplierRequest request)
    {
        try
        {
            var supplier = _mapper.Map<Supplier>(request);
            var result = await _supplierRepository.UpdateSupplierAsync(supplier);
            if (!result)
            {
                return ErrorOr<bool>.NotFound();
            }
            
            return ErrorOr<bool>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<bool>.InternalServerError();
        }
    }
}
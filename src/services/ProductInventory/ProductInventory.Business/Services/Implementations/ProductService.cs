using AutoMapper;
using Common.ErrorHandling;
using ProductInventory.Business.DTOs.Product;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<int>> CreateProductAsync(CreateProductRequest request)
    {
        try
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productRepository.CreateProductAsync(product);
            return ErrorOr<int>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<int>.InternalServerError("Internal Server Error. Either supplier or category were not found."); 
        }
    }
    
    public async Task<ErrorOr<IReadOnlyList<ProductDto>>> GetProductsAsync(GetProductsRequest request)
    {
        try
        {
            var result = await _productRepository.GetProductsAsync(request.PageNumber, request.PageSize);
            var response = result.Select(p => _mapper.Map<ProductDto>(p)).ToList().AsReadOnly();
            return ErrorOr<IReadOnlyList<ProductDto>>.Ok(response);
        }
        catch (Exception e)
        {
            return ErrorOr<IReadOnlyList<ProductDto>>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<ProductDto>> GetProductByIdAsync(GetProductByIdRequest request)
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product is null)
            {
                return ErrorOr<ProductDto>.NotFound();
            }
            
            return ErrorOr<ProductDto>.Ok(_mapper.Map<ProductDto>(product));
        }
        catch (Exception e)
        {
            return ErrorOr<ProductDto>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<bool>> DeleteProductAsync(DeleteProductRequest request)
    {
        try
        {
            var result = await _productRepository.DeleteProductAsync(request.Id);
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
    
    public async Task<ErrorOr<bool>> UpdateProductAsync(UpdateProductRequest request)
    {
        try
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productRepository.UpdateProductAsync(product);
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
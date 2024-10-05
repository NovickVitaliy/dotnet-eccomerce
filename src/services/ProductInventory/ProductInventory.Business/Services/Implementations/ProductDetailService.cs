using AutoMapper;
using Common.ErrorHandling;
using ProductInventory.Business.DTOs.ProductDetail;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.Services.Implementations;

public class ProductDetailService : IProductDetailService
{
    private readonly IProductDetailsRepository _productDetailsRepository;
    private readonly IMapper _mapper;
    
    public ProductDetailService(IProductDetailsRepository productDetailsRepository, IMapper mapper)
    {
        _productDetailsRepository = productDetailsRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<int>> CreateProductDetailAsync(CreateProductDetailRequest request)
    {
        try
        {
            var productDetail = _mapper.Map<ProductDetail>(request);
            var result = await _productDetailsRepository.CreateProductDetailAsync(productDetail);
            return ErrorOr<int>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<int>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<IReadOnlyList<ProductDetailDto>>> GetProductDetailsAsync(GetProductDetailsRequest request)
    {
        try
        {
            var result = await _productDetailsRepository.GetProductDetailsAsync(request.PageNumber, request.PageSize);
            var response = result.Select(pd => _mapper.Map<ProductDetailDto>(pd)).ToList().AsReadOnly();
            return ErrorOr<IReadOnlyList<ProductDetailDto>>.Ok(response);
        }
        catch (Exception e)
        {
            return ErrorOr<IReadOnlyList<ProductDetailDto>>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<ProductDetailDto>> GetProductDetailsByIdAsync(GetProductDetailByIdRequest request)
    {
        try
        {
            var productDetail = await _productDetailsRepository.GetProductDetailByIdAsync(request.Id);
            if (productDetail is null)
            {
                return ErrorOr<ProductDetailDto>.NotFound();
            }
            var productDetailDto = _mapper.Map<ProductDetailDto>(productDetail);
            return ErrorOr<ProductDetailDto>.Ok(productDetailDto);
        }
        catch (Exception e)
        {
            return ErrorOr<ProductDetailDto>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<bool>> DeleteProductDetailAsync(DeleteProductDetailRequest request)
    {
        try
        {
            var success = await _productDetailsRepository.DeleteProductDetailAsync(request.Id);
            if (!success)
            {
                return ErrorOr<bool>.NotFound();
            }
            
            return ErrorOr<bool>.Ok(success);
        }
        catch (Exception e)
        {
            return ErrorOr<bool>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<bool>> UpdateProductDetailAsync(UpdateProductDetailRequest request)
    {
        try
        {
            var productDetail = _mapper.Map<ProductDetail>(request);
            var success = await _productDetailsRepository.UpdateProductDetailAsync(productDetail);
            if (!success)
            {
                return ErrorOr<bool>.NotFound();
            }
            
            return ErrorOr<bool>.Ok(success);
        }
        catch (Exception e)
        {
            return ErrorOr<bool>.InternalServerError();
        }
    }
}
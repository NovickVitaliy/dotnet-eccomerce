using AutoMapper;
using Common.ErrorHandling;
using ProductInventory.Business.DTOs.ProductTag;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.Services.Implementations;

public class ProductTagService : IProductTagService
{
    private readonly IProductTagRepository _productTagRepository;
    private readonly IMapper _mapper;
    
    public ProductTagService(IProductTagRepository productTagRepository, IMapper mapper)
    {
        _productTagRepository = productTagRepository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<int>> CreateProductTagAsync(CreateProductTagRequest request)
    {
        try
        {
            var productTag = _mapper.Map<ProductTag>(request);
            var result = await _productTagRepository.CreateProductTagAsync(productTag);
            return ErrorOr<int>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<int>.InternalServerError(); 
        }    
    }
    
    public async Task<ErrorOr<IReadOnlyList<ProductTagDto>>> GetProductTagsAsync(GetProductTagsRequest request)
    {
        try
        {
            var result = await _productTagRepository.GetProductTagsAsync(request.PageNumber, request.PageSize);
            var response = result.Select(pt => _mapper.Map<ProductTagDto>(pt)).ToList().AsReadOnly();
            return ErrorOr<IReadOnlyList<ProductTagDto>>.Ok(response);
        }
        catch (Exception e)
        {
            return ErrorOr<IReadOnlyList<ProductTagDto>>.InternalServerError();
        }
    }
    
    public async Task<ErrorOr<ProductTagDto>> GetProductTagByIdAsync(GetProductTagByIdRequest request)
    {
        try
        {
            var result = await _productTagRepository.GetProductTagByIdAsync(request.Id);
            if (result is null)
            {
                return ErrorOr<ProductTagDto>.NotFound();
            }
            
            return ErrorOr<ProductTagDto>.Ok(_mapper.Map<ProductTagDto>(result));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw; 
        }
    }
    
    public async Task<ErrorOr<bool>> DeleteProductTagAsync(DeleteProductTagRequest request)
    {
        try
        {
            var result = await _productTagRepository.DeleteProductTagAsync(request.Id);
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
    
    public async Task<ErrorOr<bool>> UpdateProductTagAsync(UpdateProductTagRequest request)
    {
        try
        {
            var productTag = _mapper.Map<ProductTag>(request);
            var result = await _productTagRepository.UpdateProductTagAsync(productTag);
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
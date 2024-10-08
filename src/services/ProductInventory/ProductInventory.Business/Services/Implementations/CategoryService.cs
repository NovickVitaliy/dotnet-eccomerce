using AutoMapper;
using Common.ErrorHandling;
using FluentValidation;
using ProductInventory.Business.DTOs.Category;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryRequest> _createCategoryRequestValidator;
    private readonly IValidator<UpdateCategoryRequest> _updateCategoryRequestValidator;
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IValidator<UpdateCategoryRequest> updateCategoryRequestValidator, IValidator<CreateCategoryRequest> createCategoryRequestValidator)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _updateCategoryRequestValidator = updateCategoryRequestValidator;
        _createCategoryRequestValidator = createCategoryRequestValidator;
    }
    public async Task<ErrorOr<int>> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var validationResult = await _createCategoryRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ErrorOr<int>.BadRequest(validationResult.Errors[0].ErrorMessage);
        }
        
        var category = _mapper.Map<Category>(request);

        try
        {
            var result = await _categoryRepository.CreateCategoryAsync(category);
            return ErrorOr<int>.Ok(result);
        }
        catch (Exception e)
        {
            return ErrorOr<int>.InternalServerError();
        }
    }

    public async Task<ErrorOr<IReadOnlyList<CategoryDto>>> GetCategoriesAsync(GetCategoriesRequest request)
    {
        try
        {
            var result = await _categoryRepository.GetCategoriesAsync(request.PageNumber, request.PageSize);
            var categoryDtos = result.Select(c => _mapper.Map<CategoryDto>(c)).ToList().AsReadOnly();
            return ErrorOr<IReadOnlyList<CategoryDto>>.Ok(categoryDtos);
        }
        catch (Exception e)
        {
            return ErrorOr<IReadOnlyList<CategoryDto>>.InternalServerError();
        }
    }
    public async Task<ErrorOr<CategoryDto>> GetCategoryByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            if (category is null)
            {
                return ErrorOr<CategoryDto>.NotFound();
            }
            return ErrorOr<CategoryDto>.Ok(_mapper.Map<CategoryDto>(category));
        }
        catch (Exception e)
        {
            return ErrorOr<CategoryDto>.InternalServerError();
        }
    }
    public async Task<ErrorOr<bool>> DeleteCategoryAsync(DeleteCategoryRequest request)
    {
        try
        {
            var result = await _categoryRepository.DeleteCategoryAsync(request.Id);
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
    public async Task<ErrorOr<bool>> UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        var validationResult = await _updateCategoryRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ErrorOr<bool>.BadRequest(validationResult.Errors[0].ErrorMessage);
        }
        try
        {
            var category = _mapper.Map<Category>(request);
            var result = await _categoryRepository.UpdateCategoryAsync(category);
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
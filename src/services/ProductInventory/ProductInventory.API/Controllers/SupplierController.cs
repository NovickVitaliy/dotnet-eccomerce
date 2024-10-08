using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.Business.DTOs.Supplier;
using ProductInventory.Business.Services.Conctracts;

namespace ProductInventory.API.Controllers;

[Route("/api/suppliers")]
public class SupplierController : BaseApiController
{
    private readonly ISupplierService _supplierService;
    
    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierRequest request)
    {
        var response = await _supplierService.CreateSupplierAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Created($"/api/suppliers/{response.Data}", new {id = response.Data});
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSuppliersRequest request)
    {
        var response = await _supplierService.GetSuppliersAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _supplierService.GetSupplierByIdAsync(new GetSupplierByIdRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSupplierRequest request)
    {
        request = request with { SupplierId = id };
        var response = await _supplierService.UpdateSupplierAsync(request);
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _supplierService.DeleteSupplierAsync(new DeleteSupplierRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }
}
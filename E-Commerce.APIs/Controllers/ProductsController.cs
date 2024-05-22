using E_Commerce.BL.Dtos;
using E_Commerce.BL.Managers.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductsController(IProductManager productManager)
    {
        _productManager = productManager;
    }


    [HttpGet]
    public ActionResult GetProducts(string? category, string? name)
    {
        try
        {
            var products = _productManager.GetProducts(category, name);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult GetProductDetails(int id)
    {
        try
        {
            var product = _productManager.GetById(id);
            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [Authorize(Policy = "AdminsOnly")]
    [HttpPost]
    public ActionResult AddProduct(ProductDto productDto)
    {
        try
        {

            _productManager.AddProduct(productDto);
            return Ok("Product added successfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpPut("{id}")]
    [Authorize(Policy = "AdminsOnly")]

    public ActionResult UpdateProduct(int id, ProductDto productDto)
    {
        try
        {

            _productManager.UpdateProduct(id, productDto);
            return Ok($"Product with ID {id} updated successfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminsOnly")]
    public ActionResult DeleteProduct(int id)
    {
        try
        {

            _productManager.DeleteProduct(id);
            return Ok($"Product with ID {id} deleted successfully");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

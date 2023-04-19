

using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class ProductsController : ControllerBase 
    {
        private readonly IProductRepository _repo;
   
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var    products= await _repo.GetProductAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
           return await _repo.GetProductByIdAsync(id);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productbrands=await _repo.GetProductBrandAsync();
            return  Ok(productbrands);
        }
         [HttpGet("Types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypess()
        {
            var producttypes=await _repo.GetProductTypeAsync();
            return  Ok(producttypes);
        }
    }
}
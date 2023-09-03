

using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class ProductsController : ControllerBase 
    {

 
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTyperepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;
   
        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTyperepo,IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTyperepo = productTyperepo;
            _productBrandRepo = productBrandRepo;
           
            
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec =new ProductsWithTypesAndBrandsSpecification();
            var    products= await _productsRepo.ListAsync(spec);
            // return Ok(products);

            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var Product =await _productsRepo.GetEntityWithSpec(spec); 
 
            return _mapper.Map<Product,ProductToReturnDto>(Product);

        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productbrands=await _productBrandRepo.ListAllAsync();
            return  Ok(productbrands);
        }
         [HttpGet("Types")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductTypess()
        {
            var producttypes=await _productTyperepo.ListAllAsync();
            return  Ok(producttypes);
        }
    }
}
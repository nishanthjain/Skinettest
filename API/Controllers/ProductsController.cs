

using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{


    
    public class ProductsController   :BaseApiController 
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec =new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec=new ProductWithFiltersForCountSpecification(productParams);
            var totalItems=await _productsRepo.CountAsync(countSpec);

            var    products= await _productsRepo.ListAsync(spec);
            var data =_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize,totalItems,data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),  StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product =await _productsRepo.GetEntityWithSpec(spec); 
            if (product==null) return NotFound(new ApiResponse(404));
 
            return _mapper.Map<Product,ProductToReturnDto>(product);

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
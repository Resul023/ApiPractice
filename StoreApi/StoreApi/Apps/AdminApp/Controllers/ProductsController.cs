using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Apps.AdminApp.DTOs.ProductDtos;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Entities;

namespace StoreApi.Apps.AdminApp.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ProductsController(StoreDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            ProductGetDto productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
            };
            if (product == null) return NotFound();

            return StatusCode(200, productDto);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(int page =1)
        {
            var products = await _context.Products.Where(x=>!x.IsDeleted).Skip((page-1)*8).Take(8).ToListAsync();
            if(products == null) return BadRequest();
            ProductListDto productList = new ProductListDto
            {
                TotalCount = _context.Products.Count(),
                Items = _context.Products.Select(x=> new ProductListItemDto { Id = x.Id,Name = x.Name,SalePrice=x.SalePrice}).ToList()
            };

            return StatusCode(200, productList);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(ProductPostDto productDto)
        {
            if (productDto == null) return BadRequest();
            Product product = new Product
            {
                Name = productDto.Name,
                SalePrice = productDto.SalePrice,
                CostPrice = productDto.CostPrice,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ProductPostDto productDto)
        {
            Product isExists = await _context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (isExists == null) return NotFound();
            if (productDto == null) return NotFound();
            isExists.SalePrice = productDto.SalePrice;
            isExists.CostPrice = productDto.CostPrice;
            isExists.Name = productDto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product =  _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            product.IsDeleted = true;
            _context.SaveChanges();

            return NoContent();
        }
    }
}

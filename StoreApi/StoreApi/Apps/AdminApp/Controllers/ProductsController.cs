using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Apps.AdminApp.DTOs.CategoryDtos;
using StoreApi.Apps.AdminApp.DTOs.ProductDtos;
using StoreApi.DATA.Entities;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Entities;

namespace StoreApi.Apps.AdminApp.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(StoreDbContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product = await _context.Products.Include(x=>x.Category).ThenInclude(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (product == null) return NotFound();
            ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);


            return StatusCode(200, productDto);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll(int page =1)
        {
            var products = await _context.Products.Include(x=>x.Category).Where(x=>!x.IsDeleted).Skip((page-1)*8).Take(8).ToListAsync();
            if(products == null) return BadRequest();
            ProductListDto productList = new ProductListDto
            {
                TotalCount = _context.Products.Count(),
                Items = products.Select(x =>
                new ProductListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SalePrice = x.SalePrice,
                    Category = new CategoryInProductListItemDto
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                        ProductCount = x.Category.Products.Count()
                    }

                }).ToList()
            };

            return StatusCode(200, productList);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(ProductPostDto productDto)
        {
            if (!await _context.Categories.AnyAsync(x => x.Id == productDto.CategoryId && !x.IsDeleted))
                return NotFound();

            if (productDto == null) return BadRequest();
            Product product = new Product
            {
                Name = productDto.Name,
                SalePrice = productDto.SalePrice,
                CostPrice = productDto.CostPrice,
                CategoryId = productDto.CategoryId
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

            if (isExists.CategoryId != productDto.CategoryId && !await _context.Categories.AnyAsync(x => x.Id == productDto.CategoryId && !x.IsDeleted))
                return NotFound();

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

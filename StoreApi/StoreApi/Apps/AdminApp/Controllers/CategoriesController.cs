using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StoreApi.Apps.AdminApp.DTOs.CategoryDtos;
using StoreApi.DATA.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication1.DAL;

namespace StoreApi.Apps.AdminApp.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public CategoriesController(StoreDbContext context)
        {
            this._context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null) return NotFound();

            CategoryGetDto categoryDto = new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt,

            };
            return Ok(categoryDto);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var query = _context.Categories.Where(x => !x.IsDeleted);
            CategoryListDto categoryListDto = new CategoryListDto
            {
                TotalCount = query.Count(),
                Items = query.Select(x=>new CategoryListItemDto { Id = x.Id,Name=x.Name}).ToList()
            };
            return Ok(categoryListDto);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryPostDto categoryDto)
        {
            if(categoryDto == null) return BadRequest();
            if (await _context.Categories.AnyAsync(x => x.Name.ToUpper() == categoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);
            Category category = new Category
            {
                Name = categoryDto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201, category);
        }
        [HttpPut("")]
        public async Task<IActionResult>Update(int Id, CategoryPostDto categoryDto)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category == null) return NotFound();

            category.Name = categoryDto.Name;
            category.ModifiedAt = DateTime.UtcNow;
            return NoContent();
        }

        

    }
}

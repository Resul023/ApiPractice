using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StoreApi.Apps.AdminApp.DTOs.CategoryDtos;
using StoreApi.DATA.Entities;
using StoreApi.DATA.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication1.DAL;

namespace StoreApi.Apps.AdminApp.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(IMapper mapper,ICategoryRepository categoryRepository)
        {
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryGetDto),200)]
        public async Task<IActionResult> Get(int id)
        {
            Category category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
            if (category == null) return NotFound();

            CategoryGetDto categoryDto = _mapper.Map<CategoryGetDto>(category);
            return Ok(categoryDto);
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(CategoryListDto), 200)]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var query = _categoryRepository.GetAll(x => !x.IsDeleted);
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
            if (await _categoryRepository.IsExistAsync(x => x.Name.ToUpper() == categoryDto.Name.Trim().ToUpper() && !x.IsDeleted))
                return StatusCode(409);
            Category category = new Category
            {
                Name = categoryDto.Name,
                IsDeleted = false
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.CommitAsync();
            return StatusCode(201, category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        /// <response code="204">Entity updated successfuly</response>
        /// <response code="400">Model is not Valid</response>
        /// <response code="404">Not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult>Update(int id, CategoryPostDto categoryDto)
        {
            Category category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
            if (category == null) return NotFound();

            category.Name = categoryDto.Name;
            category.ModifiedAt = DateTime.UtcNow;
            await _categoryRepository.CommitAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _categoryRepository.GetAsync(x =>!x.IsDeleted && x.Id == id);
            if (category == null) return NotFound();
            category.IsDeleted = true;
            await _categoryRepository.CommitAsync();
            return NoContent();
        }

        

    }
}

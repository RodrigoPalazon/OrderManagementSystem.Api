using Microsoft.AspNetCore.Mvc;
using OMS.Model;
using OMS.Services.Interfaces;
using OMS.Api.DTOs;

namespace OMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    Name = request.Name ?? string.Empty,
                    Description = request.Description ?? string.Empty
                };

                _categoryService.CreateCategory(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest("Route id and category id must match.");

            try
            {
                _categoryService.UpdateCategory(category);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
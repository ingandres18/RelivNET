using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RelivNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("{Id:int}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetCategoryById(int Id)
        {
            return Ok(await _categoryRepository.GetCategoryByIdAsync(Id));
        }

        [HttpGet(Name = "GetAllCategory")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            return Ok(await _categoryRepository.GetCategoryAsync());
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            _categoryRepository.AddCategory(category);
            return Ok(category);
        }

        [HttpDelete("{Id:int}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            _categoryRepository.DeleteCategory(Id);
            return Ok();
        }

        [HttpPut(Name = "UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            _categoryRepository.UpdateCategory(category);
            return Ok(category);
        }
    }
}

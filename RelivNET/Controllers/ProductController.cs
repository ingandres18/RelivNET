using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RelivNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{Id:int}", Name = "GetProducts")]
        public async Task<ActionResult<Product>> GetProductsById(int Id)
        {
            return Ok(await _productRepository.GetProductByIdAsync(Id));
        }

        [HttpGet(Name = "GeAlltProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await _productRepository.GetProductAsync());
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
             _productRepository.AddProduct(product);
            return Ok(product);
        }

        [HttpDelete("{Id:int}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            _productRepository.DeleteProduct(Id);
            return Ok();
        }

        [HttpPut(Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            _productRepository.UpdateProduct(product);
            return Ok(product);
        }
    }
}

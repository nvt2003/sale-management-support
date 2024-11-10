using Microsoft.AspNetCore.Mvc;
using Objects.Models;
using Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/product/count/page
        [HttpGet("{count}/{page}")]
        public async Task<ActionResult<List<Product>>> GetSomeProducts(int count, int page)
        {
            var products = await _productRepository.GetSomeProductsAsync(count, page);
            return Ok(products);
        }

        // GET: api/product/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> GetProductById(string code)
        {
            var product = await _productRepository.GetProductByIdAsync(code);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var createdProduct = await _productRepository.AddProductAsync(product);
            return Ok(createdProduct);
        }

        // PUT: api/product/{code}
        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateProduct(int code, Product product)
        {
            if (code.Equals(product.ProductCode))
            {
                return BadRequest();
            }

            var updatedProduct = await _productRepository.UpdateProductAsync(product);
            return Ok(updatedProduct);
        }

        // DELETE: api/product/{code}
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteProduct(string code)
        {
            var result = await _productRepository.DeleteProductAsync(code);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

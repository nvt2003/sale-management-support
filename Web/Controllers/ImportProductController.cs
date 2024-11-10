using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Objects.Models;
using Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportProductController : Controller
    {
        private readonly IImportProductRepository _importProductRepository;

        public ImportProductController(IImportProductRepository importProductRepository)
        {
            _importProductRepository = importProductRepository;
        }

        // GET: api/importproducts
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<ImportProduct>>> GetAllImportProducts()
        {
            var importProducts = await _importProductRepository.GetAllImportProductsAsync();
            return Ok(importProducts);
        }

        // GET: api/importproducts/count/page
        [HttpGet("{count}/{page}")]
        public async Task<ActionResult<List<ImportProduct>>> GetSomeImportProducts(int count, int page)
        {
            var importProducts = await _importProductRepository.GetSomeImportProductsAsync(count, page);
            return Ok(importProducts);
        }

        // GET: api/importproducts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportProduct>> GetImportProductById(int id)
        {
            var importProduct = await _importProductRepository.GetImportProductsByIdAsync(id);
            if (importProduct == null)
            {
                return NotFound();
            }
            return Ok(importProduct);
        }

        // POST: api/importproducts
        [HttpPost]
        public async Task<ActionResult<ImportProduct>> AddImportProduct(ImportProduct importProduct)
        {
            var createdImportProduct = await _importProductRepository.AddImportProductsAsync(importProduct);
            return Ok(createdImportProduct);
        }

        // PUT: api/importproducts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImportProduct(int id, ImportProduct importProduct)
        {
            if (id != importProduct.Id)
            {
                return BadRequest();
            }

            var updatedImportProduct = await _importProductRepository.UpdateImportProductsAsync(importProduct);
            if (updatedImportProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedImportProduct);
        }

        // DELETE: api/importproducts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportProduct(int id)
        {
            var result = await _importProductRepository.DeleteImportProductsAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

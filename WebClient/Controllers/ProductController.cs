using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Objects.DTOs;
using Objects.Models;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string ApiUrl;
        private CategoryController _categoryController;

        public ProductController(IMapper mapper, CategoryController categoryController)
        {
            _mapper = mapper;
            _categoryController = categoryController;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/product";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<Product>>(ApiUrl);
                return View(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Paginated(int count, int page)
        {
            try
            {
                var paginatedProducts = await _httpClient.GetFromJsonAsync<List<Product>>($"{ApiUrl}/{count}/{page}");
                return View(paginatedProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Detail(string productCode)
        {
            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>($"{ApiUrl}/{productCode}");
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProductDTO productDto = new ProductDTO();
            productDto.ProductCode = "P0001";
            ViewData["Categories"] = _categoryController.GetCategory().Result;
            return View(productDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}", product);
                if (response.IsSuccessStatusCode)
                {
                    var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
                    return RedirectToAction("Detail", new { productCode = createdProduct.ProductCode });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create product.");
                    ViewData["Categories"] = _categoryController.GetCategory().Result;
                    return View(productDTO);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string productCode)
        {
            try
            {
                Product product = await _httpClient.GetFromJsonAsync<Product>($"{ApiUrl}/{productCode}");
                ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
                ViewData["Categories"] = _categoryController.GetCategory().Result;
                return View(productDTO);
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string productCode, Product product)
        {
            try
            {
                if (productCode != product.ProductCode)
                {
                    return BadRequest();
                }

                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{productCode}", product);
                if (response.IsSuccessStatusCode)
                {
                    var updatedProduct = await response.Content.ReadFromJsonAsync<Product>();
                    return RedirectToAction("Detail", new { productCode = updatedProduct.ProductCode });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update product.");
                    ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
                    ViewData["Categories"] = _categoryController.GetCategory().Result;
                    return View(productDTO);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string productCode)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{productCode}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete product.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
        public async Task<List<Product>> GetProduct()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(ApiUrl);
            return products;
        }
        public async Task<IActionResult> GetProductById(string ProductId)
        {
            Product product = await _httpClient.GetFromJsonAsync<Product>($"{ApiUrl}/{ProductId}");
            return Json(product);
        }
    }
}

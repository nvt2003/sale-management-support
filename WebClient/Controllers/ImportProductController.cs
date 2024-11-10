using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Objects.DTOs;
using Objects.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class ImportProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string ApiUrl;
        ProductController _productController;

        public ImportProductController(IMapper mapper, ProductController productController)
        {
            _mapper = mapper;
            _productController = productController;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/ImportProduct";
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ImportProduct> importProducts = await _httpClient.GetFromJsonAsync<List<ImportProduct>>(ApiUrl);
                return View(importProducts);
            }
            catch (Exception ex)
            {
                return View("Error", new { Message = ex.Message });
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var importProduct = JsonSerializer.Deserialize<ImportProductDTO>(jsonResponse);
                    return View(importProduct); // Pass data to view
                }

                return NotFound(); // Return 404 if the product was not found
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return View("Error", new { Message = ex.Message });
            }
        }
        public IActionResult Create()
        {
            ViewData["ProductCodes"] = _productController.GetProduct().Result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImportProductDTO importProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ImportProduct importProduct = _mapper.Map<ImportProduct>(importProductDto);
                    var response = await _httpClient.PostAsJsonAsync(ApiUrl,importProduct);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create the product. Please try again.");
                        ViewData["ProductCodes"] = _productController.GetProduct().Result;
                        return View(importProductDto);
                    }
                }
                else
                {
                    ViewData["ProductCodes"] = _productController.GetProduct().Result;
                    return View(importProductDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(importProductDto);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var importProduct = JsonSerializer.Deserialize<ImportProductDTO>(jsonResponse);
                    return View(importProduct); // Pass the product details to the view
                }

                return NotFound(); // Return 404 if product is not found
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., network issues)
                return View("Error", new { Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ImportProductDTO importProductDto)
        {
            try
            {
                if (id != importProductDto.Id)
                {
                    // If the id in the URL doesn't match the id in the form data, return a BadRequest
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    // Send the updated product data to the API using PUT
                    var jsonContent = JsonSerializer.Serialize(importProductDto);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = await _httpClient.PutAsync($"{ApiUrl}/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to the Index page (or any other page after successful update)
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Handle failure - maybe return to the form with an error message
                        ModelState.AddModelError("", "Failed to update the product. Please try again.");
                        ViewData["ProductCodes"] = _productController.GetProduct().Result;
                        return View(importProductDto); // Return the form with error message
                    }
                }
                else
                {
                    // If the model is not valid, return the user to the form with validation errors
                    ViewData["ProductCodes"] = _productController.GetProduct().Result;
                    return View(importProductDto);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                ViewData["ProductCodes"] = _productController.GetProduct().Result;
                return View(importProductDto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var importProduct = JsonSerializer.Deserialize<ImportProductDTO>(jsonResponse);
                    return View(importProduct); // Pass the product details to the view for confirmation
                }

                return NotFound(); // Return 404 if product is not found
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., network issues)
                return View("Error", new { Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Send DELETE request to the API
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to the Index page after successful deletion
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle failure - maybe return to the list page with an error message
                    ModelState.AddModelError("", "Failed to delete the product. Please try again.");
                    return RedirectToAction("Index"); // Return to the list page on failure
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues)
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return RedirectToAction("Index"); // Return to the list page with error message
            }
        }
        public async Task<IActionResult> GetNumberOfProduct(string ProductId)
        {
            var response = await _httpClient.GetAsync("https://localhost:7260/api/ImportProduct?$filter=ProductId eq '"+ProductId+"'&$apply=groupby((ProductId), aggregate(Quantity with sum as TotalQuantity))");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            //var result = JsonSerializer.Deserialize<ProductData>(jsonResponse);
            return Json(jsonResponse);
        }
        public class ProductData
        {
            public string ProductId { get; set; }
            public int TotalQuantity { get; set; }
        }
    }
}

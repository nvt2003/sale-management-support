using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Objects.DTOs;
using Objects.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string ApiUrl;
        public CategoryController(IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient(); 
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/category";
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>(ApiUrl);
            return View(categories);
        }
        public async Task<List<Category>> GetCategory()
        {
            List<Category> categories = await _httpClient.GetFromJsonAsync<List<Category>>(ApiUrl);
            return categories;
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _httpClient.GetFromJsonAsync<Category>($"{ApiUrl}/{id}");
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }catch (Exception ex)
            {
                ViewBag.Message = ex.Message; 
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = _mapper.Map<Category>(categoryDTO);
                var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}", newCategory);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Add successfully!";
                    return View(categoryDTO);
                }
            }
            ViewBag.Message = "Add failed!";
            return View(categoryDTO);
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _httpClient.GetFromJsonAsync<Category>($"{ApiUrl}/{id}");
                if (category == null)
                {
                    return NotFound();
                }
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
                return View(categoryDTO);
            }catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                ViewBag.Message = "Not found any category with id "+id+"!";
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", categoryDTO);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Update successfully!";
                    return View(categoryDTO);
                }
            }
            ViewBag.Message = "Update failed!";
            return View(categoryDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Category>($"{ApiUrl}/{id}");
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}

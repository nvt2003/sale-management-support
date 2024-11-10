using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Objects.DTOs;
using Objects.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string ApiUrl;

        public CustomerController(IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/customer";
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var customers = await _httpClient.GetFromJsonAsync<List<Customer>>(ApiUrl);
                return View(customers);
            }catch (Exception ex)
            {
                return View(new List<Customer>());
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<Customer>($"{ApiUrl}/{id}");
                return View(customer);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, customerDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create customer. Please try again.");
                }
            }

            return View(customerDTO); 
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var customer = await _httpClient.GetFromJsonAsync<Customer>($"{ApiUrl}/{id}");
                return View(customer);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.ID)
            {
                return BadRequest(); // If the ID doesn't match, return a bad request
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", customerDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update customer. Please try again.");
                }
            }

            return View(customerDTO);
        }

    }
}

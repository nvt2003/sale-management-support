using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Objects.DTOs;
using Objects.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebClient.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly MyDBShopContext _context;
        private readonly IMapper _mapper;
        private string ApiUrl;
        ProductController _productController;

        public InvoiceController(IMapper mapper, MyDBShopContext context, ProductController productController)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/invoice";
            _context = context;
            _productController = productController;
        }
        public async Task<IActionResult> Index()
        {
            List<Invoice> invoices = await _httpClient.GetFromJsonAsync<List<Invoice>>(ApiUrl);
            return View(invoices);

        }

        public async Task<IActionResult> Details(int id)
        {
            Invoice invoice = await _httpClient.GetFromJsonAsync<Invoice>($"{ApiUrl}/{id}");
            ViewData["ProductCodes"] = _productController.GetProduct().Result;
            return View(invoice);
        }

        public IActionResult Create()
        {
            InvoiceDTO invoiceDTO = new InvoiceDTO();
            invoiceDTO.InvoiceDate = DateTime.Now;
            invoiceDTO.TotalAmount = 0;
            return View(invoiceDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                Invoice newInvoice= _mapper.Map<Invoice>(invoiceDTO);
                //_context.Invoices.Add(newInvoice);
                var response = await _httpClient.PostAsJsonAsync($"{ApiUrl}", newInvoice);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }

            return View(invoiceDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var invoiceJson = await response.Content.ReadAsStringAsync();
                var invoice = JsonSerializer.Deserialize<Invoice>(invoiceJson);
                var invoiceViewModel = _mapper.Map<InvoiceDTO>(invoice);
                return View(invoiceViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvoiceDTO invoiceViewModel)
        {
            if (id != invoiceViewModel.InvoiceId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var invoice = _mapper.Map<Invoice>(invoiceViewModel);
                var invoiceJson = JsonSerializer.Serialize(invoice);
                var content = new StringContent(invoiceJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{ApiUrl}/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }

            return View(invoiceViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var invoiceJson = await response.Content.ReadAsStringAsync();
                var invoice = JsonSerializer.Deserialize<Invoice>(invoiceJson);
                var invoiceViewModel = _mapper.Map<InvoiceDTO>(invoice);
                return View(invoiceViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
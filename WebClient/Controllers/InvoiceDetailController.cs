using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Objects.Models;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class InvoiceDetailController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string ApiUrl;

        public InvoiceDetailController(IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7260/api/InvoiceDetail";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var invoiceDetails = await _httpClient.GetFromJsonAsync<List<InvoiceDetail>>(ApiUrl);
                return View(invoiceDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var invoiceDetail = await _httpClient.GetFromJsonAsync<InvoiceDetail>($"{ApiUrl}/{id}");
                if (invoiceDetail == null)
                {
                    return NotFound();
                }
                return View(invoiceDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string InvoiceId, string ProductId, string Quantity, string UnitPrice)
        {
            try
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = int.Parse(InvoiceId);
                invoiceDetail.ProductId = ProductId;
                invoiceDetail.Quantity = int.Parse(Quantity);
                invoiceDetail.UnitPrice = decimal.Parse(UnitPrice);
                var response = await _httpClient.PostAsJsonAsync(ApiUrl, invoiceDetail);
                if (response.IsSuccessStatusCode)
                {
                    return Json(response);
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create invoice detail.");
                    return StatusCode(500, "Internal server error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, InvoiceDetail invoiceDetail)
        {
            try
            {
                if (id != invoiceDetail.InvoiceDetailId)
                {
                    return BadRequest();
                }

                var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", invoiceDetail);
                if (response.IsSuccessStatusCode)
                {
                    var updatedInvoiceDetail = await response.Content.ReadFromJsonAsync<InvoiceDetail>();
                    return RedirectToAction("Detail", new { id = updatedInvoiceDetail.InvoiceDetailId });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update invoice detail.");
                    return View(invoiceDetail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        // Action to call the "Delete" API (DELETE invoice detail by ID)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete invoice detail.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}

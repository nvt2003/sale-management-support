using Microsoft.AspNetCore.Mvc;
using Objects.Models;
using Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : Controller
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public InvoiceDetailController(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        // GET: api/invoice
        [HttpGet]
        public async Task<ActionResult<List<InvoiceDetail>>> GetAllInvoiceDetails()
        {
            var invoiceDetails = await _invoiceDetailRepository.GetAllInvoiceDetailsAsync();
            return Ok(invoiceDetails);
        }

        // GET: api/invoice/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetail>> GetInvoiceDetailById(int id)
        {
            var invoiceDetail = await _invoiceDetailRepository.GetInvoiceDetailByIdAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            return Ok(invoiceDetail);
        }

        // POST: api/invoice
        [HttpPost]
        public async Task<ActionResult<InvoiceDetail>> AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            InvoiceDetail createInvoiceDetail = await _invoiceDetailRepository.AddInvoiceDetailAsync(invoiceDetail);
            return Ok(createInvoiceDetail);
        }

        // PUT: api/invoice/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceDetail(int id, InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.InvoiceDetailId)
            {
                return BadRequest();
            }

            InvoiceDetail updateInvoiceDetail = await _invoiceDetailRepository.UpdateInvoiceDetailAsync(invoiceDetail);
            return Ok(updateInvoiceDetail);
        }

        // DELETE: api/invoice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            await _invoiceDetailRepository.DeleteInvoiceDetailAsync(id);
            return Ok();
        }
    }
}

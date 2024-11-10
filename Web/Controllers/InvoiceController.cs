using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Objects.Models;
using Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        // GET: api/invoice
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<Invoice>>> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        // GET: api/invoice/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // POST: api/invoice
        [HttpPost]
        public async Task<ActionResult<Invoice>> AddInvoice(Invoice invoice)
        {
            Invoice createInvoice = await _invoiceRepository.AddInvoiceAsync(invoice);
            return Ok(createInvoice);
        }

        // PUT: api/invoice/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return BadRequest();
            }

            Invoice updateInvoice = await _invoiceRepository.UpdateInvoiceAsync(invoice);
            return Ok(updateInvoice);
        }

        // DELETE: api/invoice/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoiceRepository.DeleteInvoiceAsync(id);
            return Ok();
        }
    }
}

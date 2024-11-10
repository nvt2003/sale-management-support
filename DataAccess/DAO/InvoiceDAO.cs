using Microsoft.EntityFrameworkCore;
using Objects.Models;

namespace DataAccess.DAO
{
    public class InvoiceDAO
    {
        private readonly MyDBShopContext _context;

        public InvoiceDAO(MyDBShopContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.InvoiceDetails)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.InvoiceDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoice = await GetInvoiceByIdAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
                return false;
            }
            return true;
        }
    }
}

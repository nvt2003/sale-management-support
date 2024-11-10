using Microsoft.EntityFrameworkCore;
using Objects.Models;

namespace DataAccess.DAO
{
    public class InvoiceDetailDAO
    {
        private readonly MyDBShopContext _context;

        public InvoiceDetailDAO(MyDBShopContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceDetail>> GetAllInvoiceDetailsAsync()
        {
            return await _context.InvoiceDetails
                .Include(id => id.Invoice)
                .Include(id => id.Product)
                .ToListAsync();
        }

        public async Task<InvoiceDetail> GetInvoiceDetailByIdAsync(int id)
        {
            return await _context.InvoiceDetails
                .Include(i => i.Invoice)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.InvoiceDetailId == id);
        }

        public async Task<InvoiceDetail> AddInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Add(invoiceDetail);
            await _context.SaveChangesAsync();
            return invoiceDetail;
        }

        public async Task<InvoiceDetail> UpdateInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Update(invoiceDetail);
            await _context.SaveChangesAsync();
            return invoiceDetail;
        }

        public async Task<bool> DeleteInvoiceDetailAsync(int id)
        {
            var invoiceDetail = await GetInvoiceDetailByIdAsync(id);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
                await _context.SaveChangesAsync();
                return false;
            }
            return true;
        }
    }
}

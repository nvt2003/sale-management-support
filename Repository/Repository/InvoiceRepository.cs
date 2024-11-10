using DataAccess.DAO;
using Objects.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDAO _invoiceDao;
        public InvoiceRepository(InvoiceDAO invoiceDao)
        {
            _invoiceDao = invoiceDao;
        }

        public Task<Invoice> AddInvoiceAsync(Invoice invoice)
        {
            return _invoiceDao.AddInvoiceAsync(invoice);
        }

        public Task<bool> DeleteInvoiceAsync(int id)
        {
            return _invoiceDao.DeleteInvoiceAsync(id);
        }

        public Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return _invoiceDao.GetAllInvoicesAsync();
        }

        public Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return _invoiceDao.GetInvoiceByIdAsync(id);
        }

        public Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            return _invoiceDao.UpdateInvoiceAsync(invoice);
        }
    }
}

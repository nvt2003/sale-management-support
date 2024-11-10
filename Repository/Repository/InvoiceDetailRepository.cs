using DataAccess.DAO;
using Objects.Models;
using Repository.Interface;

namespace Repository.Repository
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly InvoiceDetailDAO _invoiceDetailDAO;
        public InvoiceDetailRepository(InvoiceDetailDAO invoiceDetailDAO)
        {
            _invoiceDetailDAO = invoiceDetailDAO;
        }

        public Task<InvoiceDetail> AddInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            return _invoiceDetailDAO.AddInvoiceDetailAsync(invoiceDetail);
        }

        public Task<bool> DeleteInvoiceDetailAsync(int id)
        {
            return _invoiceDetailDAO.DeleteInvoiceDetailAsync(id);
        }

        public Task<List<InvoiceDetail>> GetAllInvoiceDetailsAsync()
        {
            return _invoiceDetailDAO.GetAllInvoiceDetailsAsync();
        }

        public Task<InvoiceDetail> GetInvoiceDetailByIdAsync(int id)
        {
            return _invoiceDetailDAO.GetInvoiceDetailByIdAsync(id);
        }

        public Task<InvoiceDetail> UpdateInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            return _invoiceDetailDAO.UpdateInvoiceDetailAsync(invoiceDetail);
        }
    }
}

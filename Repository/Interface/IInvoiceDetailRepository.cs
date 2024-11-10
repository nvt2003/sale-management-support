using Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IInvoiceDetailRepository
    {
        Task<List<InvoiceDetail>> GetAllInvoiceDetailsAsync();
        Task<InvoiceDetail> GetInvoiceDetailByIdAsync(int id);
        Task<InvoiceDetail> AddInvoiceDetailAsync(InvoiceDetail invoiceDetail);
        Task<InvoiceDetail> UpdateInvoiceDetailAsync(InvoiceDetail invoiceDetail);
        Task<bool> DeleteInvoiceDetailAsync(int id);
    }
}

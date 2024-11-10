using Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IImportProductRepository
    {
        Task<ImportProduct> AddImportProductsAsync(ImportProduct importProduct);
        Task<ImportProduct> GetImportProductsByIdAsync(int id);
        Task<List<ImportProduct>> GetAllImportProductsAsync();
        Task<List<ImportProduct>> GetSomeImportProductsAsync(int count, int page);
        Task<ImportProduct> UpdateImportProductsAsync(ImportProduct importProduct);
        Task<bool> DeleteImportProductsAsync(int id);
    }
}

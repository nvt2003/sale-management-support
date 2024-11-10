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
    public class ImportProductRepository:IImportProductRepository
    {
        private readonly ImportProductDAO _importProductDAO;
        public ImportProductRepository(ImportProductDAO importProductDAO)
        {
            _importProductDAO = importProductDAO;
        }

        public Task<ImportProduct> AddImportProductsAsync(ImportProduct importProduct)
        {
            return _importProductDAO.AddImportProductAsync(importProduct);
        }

        public Task<bool> DeleteImportProductsAsync(int id)
        {
            return _importProductDAO.DeleteImportProductAsync(id);
        }

        public Task<List<ImportProduct>> GetAllImportProductsAsync()
        {
            return _importProductDAO.GetAllImportProductsAsync();
        }

        public Task<List<ImportProduct>> GetSomeImportProductsAsync(int count, int page)
        {
            return _importProductDAO.GetSomeImportProductsAsync(count, page);
        }

        public Task<ImportProduct> GetImportProductsByIdAsync(int id)
        {
            return _importProductDAO.GetImportProductByIdAsync(id);
        }

        public Task<ImportProduct> UpdateImportProductsAsync(ImportProduct importProduct)
        {
            return _importProductDAO.UpdateImportProductAsync(importProduct);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ImportProductDAO
    {
        private readonly MyDBShopContext _context;

        public ImportProductDAO(MyDBShopContext context)
        {
            _context = context;
        }

        public async Task<ImportProduct> AddImportProductAsync(ImportProduct importProduct)
        {
            if (importProduct == null)
                throw new ArgumentNullException(nameof(importProduct));

            await _context.ImportProducts.AddAsync(importProduct);
            await _context.SaveChangesAsync();
            return importProduct;
        }

        public async Task<ImportProduct> GetImportProductByIdAsync(int id)
        {
            return await _context.ImportProducts
                .Include(ip => ip.Product) // Include related Product data
                .FirstOrDefaultAsync(ip => ip.Id == id);
        }

        public async Task<List<ImportProduct>> GetAllImportProductsAsync()
        {
            return await _context.ImportProducts
                .Include(ip => ip.Product)
                .ToListAsync();
        }
        public async Task<List<ImportProduct>> GetSomeImportProductsAsync(int count, int page)
        {
            return await _context.ImportProducts
                .Include(ip => ip.Product)
                .Skip((page-1)*count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<ImportProduct> UpdateImportProductAsync(ImportProduct importProduct)
        {
            if (importProduct == null)
                throw new ArgumentNullException(nameof(importProduct));

            _context.ImportProducts.Update(importProduct);
            await _context.SaveChangesAsync();
            return importProduct;
        }

        public async Task<bool> DeleteImportProductAsync(int id)
        {
            var importProduct = await _context.ImportProducts.FindAsync(id);
            if (importProduct == null) return false;

            _context.ImportProducts.Remove(importProduct);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

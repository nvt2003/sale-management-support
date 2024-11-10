using Objects.Models;
using Objects.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private readonly MyDBShopContext _context;

        public ProductDAO(MyDBShopContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(string code)
        {
            return _context.Products.Where(p => p.ProductCode.Equals(code)).FirstOrDefault();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p=>p.Category).ToListAsync();
        }
        public async Task<List<Product>> GetSomeProductsAsync(int count, int page)
        {
            return await _context.Products
                .Skip((page-1)*count)
                .Take(count).ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(string code)
        {
            var product = _context.Products.Where(p => p.ProductCode.Equals(code)).FirstOrDefault();
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(string code);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetSomeProductsAsync(int count, int page);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string code);
    }
}

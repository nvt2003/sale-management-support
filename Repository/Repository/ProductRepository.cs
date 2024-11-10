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
    public class ProductRepository:IProductRepository
    {
        private readonly ProductDAO _productDAO;
        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public Task<Product> AddProductAsync(Product product)
        {
            return _productDAO.AddProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(string code)
        {
            return _productDAO.DeleteProductAsync(code);
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return _productDAO.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(string code)
        {
            return _productDAO.GetProductByIdAsync(code);
        }

        public Task<List<Product>> GetSomeProductsAsync(int count, int page)
        {
            return _productDAO.GetSomeProductsAsync(count, page);
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            return _productDAO.UpdateProductAsync(product);
        }
    }
}

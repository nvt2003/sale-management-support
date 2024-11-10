using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Objects.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }
        public Task<Category> AddCategoryAsync(Category category)
        {
            return _categoryDAO.AddCategoryAsync(category);
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            return _categoryDAO.DeleteCategoryAsync(id);
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return _categoryDAO.GetAllCategoriesAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _categoryDAO.GetCategoryByIdAsync(id);
        }

        public Task<Category> UpdateCategoryAsync(Category category)
        {
            return _categoryDAO.UpdateCategoryAsync(category);
        }
    }
}

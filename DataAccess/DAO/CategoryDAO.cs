using Microsoft.EntityFrameworkCore;
using Objects.Models;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private readonly MyDBShopContext _context;

        public CategoryDAO(MyDBShopContext context)
        {
            _context = context;
        }


        public async Task<Category> AddCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                throw new ArgumentNullException(nameof(category));
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }catch (Exception ex)
            {
                return null;
            }
        }

        // Read
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            try
            {
                if (category == null)
                    throw new ArgumentNullException(nameof(category));

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null) return false;

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

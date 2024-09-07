using Microsoft.EntityFrameworkCore;
using EcommerceApp.Data;
using EcommerceApp.Model.Domain;
using EcommerceApp.Repository.IRepositoy;

namespace EcommerceApp.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddCategory(Category category)
        {
            await _applicationDbContext.Categories.AddAsync(category);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _applicationDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _applicationDbContext.Categories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var oldCategory = await _applicationDbContext.Categories.FindAsync(category.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
                _applicationDbContext.Categories.Update(oldCategory);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the category was not found
                // Log a warning or throw an exception as needed
                throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (id != 0)
            {
                var category = await _applicationDbContext.Categories.FindAsync(id);
                if (category != null)
                {
                    _applicationDbContext.Categories.Remove(category);
                    await _applicationDbContext.SaveChangesAsync();
                }
                else
                {
                    // Handle the case where the category was not found
                    // Log a warning or throw an exception as needed
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
            }
        }
    }
}

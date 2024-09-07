using EcommerceApp.Model.Domain;


namespace EcommerceApp.Repository.IRepositoy;

public interface ICategoryRepo
{
    public Task AddCategory(Category category);
    public Task<List<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryByIdAsync(int id);
    public Task UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(int Id);
    
}
using EcommerceApp.Model;
using EcommerceApp.Model.Domain;

namespace EcommerceApp.Repository.IRepository;

public interface IProductRepo
{
    public Task AddProduct(Product category);
    public Task<List<Product>> GetProductAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task UpdateProductAsync(Product product);
    public Task DeleteProductAsync(int id);
    
}
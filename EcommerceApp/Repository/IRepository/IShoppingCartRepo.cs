using EcommerceApp.Model.Domain;

namespace EcommerceApp.Repository.IRepository;

public interface IShoppingCartRepo
{
    public Task<bool> UpdateCartAsync(string userId, int productId, int updateCount);
    public Task<List<ShoppingCart>> GetCartItemsAsync(string? userId);
    public Task<bool> ClearCartAsync(string userId);
    public Task<int> CartItemsCountAsync(string userId);
}
using EcommerceApp.Data;
using EcommerceApp.Model.Domain;
using EcommerceApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository;

public class ShoppingCartRepo: IShoppingCartRepo
{
    private readonly ApplicationDbContext _context;

    public ShoppingCartRepo(ApplicationDbContext context )
    {
        _context = context;
    }
    public async Task<bool> UpdateCartAsync(string userId, int productId, int updateCount)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return false;
        }
    
        var cart=await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId&& c.ProductId == productId);

        if (cart == null)
        {
            // Create a new cart entry if it doesn't exist
            cart = new ShoppingCart { UserId = userId, ProductId = productId, Count = updateCount };
            await _context.ShoppingCarts.AddAsync(cart);
        }
        else
        {
            // Update the existing cart entry
            cart.Count += updateCount;

            // Remove the cart if the count is 0 or less
            if (cart.Count <= 0)
            {
                _context.ShoppingCarts.Remove(cart);
            }
        }

        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<List<ShoppingCart>> GetCartItemsAsync(string? userId)
    {
        return await _context.ShoppingCarts.
            Where(u=>u.UserId==userId).Include(x=>x.Product).
            ToListAsync();
    }

    public async Task<bool> ClearCartAsync(string userId)
    {
        var cartItems = await _context.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
        _context.ShoppingCarts.RemoveRange(cartItems);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<int> CartItemsCountAsync(string userId)
    {
        var cartItems =await _context.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
        int cartCount = 0;
        foreach (var item in cartItems)
        {
            cartCount += item.Count;
        }

        return cartCount;
    }
}
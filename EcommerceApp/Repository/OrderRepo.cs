using EcommerceApp.Data;
using EcommerceApp.Model.Domain;
using EcommerceApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository;

public class OrderRepo : IOrderRepo
{
    private readonly ApplicationDbContext _context;

    public OrderRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateOrderAsync(OrderHeader order)
    {
    order.OrderDate = DateTime.Now;
     await _context.OrderHeaders.AddAsync(order);
     await _context.SaveChangesAsync();
    
    }

    public async Task<OrderHeader> GetOrderByIdAsync(int id)
    {
       var orderById = await _context.OrderHeaders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
       return orderById;
    }

    public async Task<OrderHeader> GetOrdersBySessionIdAsync(string sessionId)
    {
       return await _context.OrderHeaders.FirstOrDefaultAsync(u=>u.SessionId==sessionId.ToString());
    }

    public async Task<List<OrderHeader>> GetOrdersAsync(string? userId = null)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            return await _context.OrderHeaders.Where(x => x.UserId == userId).ToListAsync();
        }

        return await _context.OrderHeaders.ToListAsync();

    }

    public async Task<OrderHeader> UpdateStatusAsync(int id, string status,string paymentIntentId)
    {
        var order = await _context.OrderHeaders.FindAsync(id);
        if (order != null)
        {
            order.Status = status;
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                order.PaymentIntentId = paymentIntentId;
            }
            await _context.SaveChangesAsync();
        }

        return order;
    }
}
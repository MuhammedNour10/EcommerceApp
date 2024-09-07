using EcommerceApp.Model.Domain;

namespace EcommerceApp.Repository.IRepository;

public interface IOrderRepo
{
    public Task CreateOrderAsync(OrderHeader order);
    public Task<OrderHeader> GetOrderByIdAsync(int id);
    
    public Task<OrderHeader> GetOrdersBySessionIdAsync(string sessionId);
    public Task<List<OrderHeader>> GetOrdersAsync(string? userId=null);
    public Task<OrderHeader> UpdateStatusAsync(int id,string status,string paymentIntentId);
    
}
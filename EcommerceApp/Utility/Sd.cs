using EcommerceApp.Model.Domain;

namespace EcommerceApp.Utility;

public  static class Sd
{
    public static string RoleAdmin = "Admin"; 
    public static string RoleCustomer = "Customer";
    public static string StatusPending = "Pending";
    public  static string StatusApproved = "Approved";
    public static string StatusReadyForPickUp = "ReadyForPickUp";
    public static string statusCompleted = "Completed";
    public static string statusCancelled = "Cancelled";

    public static List<OrderDetails> ConvertShoppingCartListToOrderDetails(List<ShoppingCart> shoppingCartList)
    {
        List<OrderDetails> orderDetails = new List<OrderDetails>();

        foreach (var cart in shoppingCartList)
        {
            OrderDetails orderDetail = new OrderDetails
            {
                ProductId = cart.ProductId,  // Corrected property name
                Count = cart.Count,
                Price = Convert.ToDouble(cart.Product.Price),
                ProductName = cart.Product.Name,
            };

            orderDetails.Add(orderDetail);
        }

        return orderDetails;
    }
    
}
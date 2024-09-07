using EcommerceApp.Model.Domain;
using EcommerceApp.Model.Domain;
using EcommerceApp.Repository.IRepository;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe.Checkout;
namespace EcommerceApp.Service;

public class PaymentService
{
    private readonly NavigationManager _navigationManager;
    private readonly IOrderRepo _orderRepo;

    public PaymentService(NavigationManager navigationManager, IOrderRepo orderRepo)
    {
        _navigationManager = navigationManager;
        _orderRepo = orderRepo;
    }

    public Session CreateCheckoutSession(OrderHeader orderHeader)
    {
        var lineItems = orderHeader.Items.Select(order => new Stripe.Checkout.SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                Currency = "TRY",
                UnitAmountDecimal = (decimal?)order.Price * 100,
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = order.ProductName,

                }


            },
            Quantity = order.Count

        }).ToList();
        var options = new Stripe.Checkout.SessionCreateOptions
        {
            SuccessUrl = $"{_navigationManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{_navigationManager.BaseUri}cart",
            LineItems = lineItems,
            Mode = "payment",
        };

        var service = new Stripe.Checkout.SessionService();
        var session = service.Create(options);
        return session;
    }

    public async Task<OrderHeader> ChechPaymentStatusAsync(string sessionId)
    {
        OrderHeader orderHeader = await _orderRepo.GetOrdersBySessionIdAsync(sessionId);
        var service = new SessionService();
        var session = service.Get(sessionId);
        if (session.PaymentStatus.ToLower() == "paid")
        {
            await _orderRepo.UpdateStatusAsync(orderHeader.Id, Sd.StatusApproved, session.PaymentStatus);
        }

        return orderHeader;
    }

}
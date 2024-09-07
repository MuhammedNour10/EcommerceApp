using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace EcommerceApp.Model.Domain;

public class OrderHeader
{
    public int  Id { get; set; }
    public string UserId { get; set; }
    [Required]
    public double OrderTotal { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public string Status { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";

    [Required]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";
    
    public string? SessionId { get; set; }
    public string? PaymentIntentId {get; set; }

    public List<OrderDetails> Items { get; set; }=new List<OrderDetails>();
}
using System.ComponentModel.DataAnnotations;
using EcommerceApp.Model.Domain;

namespace EcommerceApp.Model.Domain;

    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000, ErrorMessage = "Price must be between 0.01 and 1000")]
        public Decimal Price { get; set; }

        public string? Description { get; set; }
        public string? SpecialTag { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
        
         

    }
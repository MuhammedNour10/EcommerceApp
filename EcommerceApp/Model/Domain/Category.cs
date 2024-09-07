
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EcommerceApp.Model.Domain;

public class Category
{ 
    
    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; } = "";

}
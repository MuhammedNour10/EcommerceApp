using EcommerceApp.Data;
using EcommerceApp.Model.Domain;
using EcommerceApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repository;

public class ProductRepo:IProductRepo
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductRepo(ApplicationDbContext applicationDbContext,IWebHostEnvironment webHostEnvironment)
    {
        _applicationDbContext = applicationDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task AddProduct(Product product)
    {
        await _applicationDbContext.Products.AddAsync(product);
        await _applicationDbContext.SaveChangesAsync();
      
        
    }

    public async Task<List<Product>> GetProductAsync()
    {
        return await _applicationDbContext.Products.Include(x=>x.Category).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _applicationDbContext.Products.FindAsync(id);
    }

   

    public async Task UpdateProductAsync(Product product)
    {
        var oldProduct = _applicationDbContext.Products.Find(product.Id);
        if (oldProduct != null)
        {
          

           
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Description = product.Description;
            oldProduct.ImageUrl = product.ImageUrl;
            oldProduct.CategoryId = product.CategoryId;

            _applicationDbContext.Products.Update(oldProduct);
            await _applicationDbContext.SaveChangesAsync();

           
        }
    }


    public async Task DeleteProductAsync(int Id)
    {
        if (Id != 0)
        {
            var product = await _applicationDbContext.Products.FindAsync(Id);
            if (product != null)
            {
                // Delete the product's image from the directory
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (File.Exists(fullImagePath))
                    {
                        File.Delete(fullImagePath);
                    }
                }

                // Remove the product from the database
                _applicationDbContext.Products.Remove(product);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }

}
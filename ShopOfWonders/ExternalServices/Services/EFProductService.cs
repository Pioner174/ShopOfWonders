using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.ExternalServices.Interfaces;
using SOW.ShopOfWonders.Models;

namespace SOW.ShopOfWonders.ExternalServices.Services
{
    public class EFProductService : IProductService
    {
        private readonly IdentityContext _context;

        public EFProductService(IdentityContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(long id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTagAsync(long tagId)
        {
            return await _context.Products
                .Where(p => p.ProductsTags.Any(pt => pt.TagId == tagId))
                .ToListAsync();
        }
    }
}

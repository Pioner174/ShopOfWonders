using SOW.DataModels;

namespace SOW.ShopOfWonders.Models.Interfaces
{
    public interface IProductService
    {

        public Task<IEnumerable<Product>> GetAllProductsAsync();

        public Task<Product> GetProductByIdAsync(long id);

        public  Task<Product> CreateProductAsync(Product product);

        public  Task UpdateProductAsync(Product product);

        public  Task DeleteProductAsync(long id);

        public  Task<IEnumerable<Product>> GetProductsByTagAsync(long tagId);
    }
}

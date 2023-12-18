using InventoryDomain;
using InverntoryData.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        bool CreateProduct(ProductModel model, Category category);
        bool UpdateProduct(Product product, ProductModel model, Category category);
    }
}
using InventoryDomain;
using InverntoryData.Models;
using InverntoryData.Repositories;
using Services.Contracts;

namespace Services.Implementation
{
    public class ProductyService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;

        public ProductyService(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productRepository.All();
        }

        public Product GetProduct(int id)
        {
            return productRepository.Get(id);
        }

        public bool CreateProduct (ProductModel model, Category category)
        {
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    price = model.price,
                    stock = model.stock,
                };

                category.Products.Add(product);

                categoryRepository.Update(category);


                categoryRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProduct (Product product, ProductModel model, Category category)
        {
            try
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.price = model.price;
                product.stock = model.stock;
                product.ProductCategory = category;



                productRepository.Update(product);

                productRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
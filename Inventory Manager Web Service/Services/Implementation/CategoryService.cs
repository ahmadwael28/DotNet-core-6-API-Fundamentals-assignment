using Inventory_Manager.Models;
using InventoryDomain;
using InverntoryData.Repositories;
using Services.Contracts;

namespace Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return categoryRepository.All();
        }

        public Category GetCategoryById(int id)
        {
            return categoryRepository.Get(id);
        }

        public Category GetCategoryWithRelatedEntities(int CategoryId)
        {
            return categoryRepository.GetWithRelatedEntities(CategoryId);
        }

        public bool CreateCategory (CategoryModel model)
        {
            var category = new Category
            {
                Name = model.Name,
            };

            categoryRepository.Add(category);

            categoryRepository.SaveChanges();

            return true;
        }
    }
}
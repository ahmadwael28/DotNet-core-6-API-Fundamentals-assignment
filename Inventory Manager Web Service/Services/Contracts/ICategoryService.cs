using Inventory_Manager.Models;
using InventoryDomain;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetCategoryWithRelatedEntities(int CategoryId);
        bool CreateCategory(CategoryModel model);
    }
}
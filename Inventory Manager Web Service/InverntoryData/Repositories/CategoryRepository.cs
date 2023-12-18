using InventoryDomain;
using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverntoryData.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(InventoryContext context) : base(context)
        {

        }

        public override void Delete(Category entity)
        {
            entity.Products.ForEach(p =>
            {
                context.Remove(p);
            });
            base.Delete(entity);
        }

        public override Category GetWithRelatedEntities(int id)
        {
            return context.Categories.Include("Products").FirstOrDefault(c => c.CategoryId == id);
        }

        public override IEnumerable<Category> AllWithRelatedEntities()
        {
            return context.Set<Category>().Include("Products").ToList();
        }
    }
}

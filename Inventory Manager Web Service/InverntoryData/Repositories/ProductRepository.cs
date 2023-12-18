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
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(InventoryContext context) : base(context)
        {

        }

        public override Product GetWithRelatedEntities(int id)
        {
            return context.Products.Include("ProductCategory").FirstOrDefault(p => p.ProductId == id);
        }
    }
}

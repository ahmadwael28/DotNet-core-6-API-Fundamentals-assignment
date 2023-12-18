using InventoryDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverntoryData.Models
{
    public class ProductModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float price { get; set; }
        public int stock { get; set; }
        public int CategoryId { get; set; }
    }
}

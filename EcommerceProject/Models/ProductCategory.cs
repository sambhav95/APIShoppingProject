using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceProject.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}

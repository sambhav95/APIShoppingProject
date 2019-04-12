using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int ProductQty { get; set; }

        public float ProductPrice { get; set; }

        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual ProductCategory Category { get; set; }
    }
}

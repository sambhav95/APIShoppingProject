using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingProject.Models
{
    public class ShopDataDbContext:DbContext
    { public DbSet<Vendor> Vendors { get; set; }
    }
}

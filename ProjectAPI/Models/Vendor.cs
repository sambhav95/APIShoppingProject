using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string EmailId { get; set; }

        public double PhoneNo { get; set; }
        public string VendorDescription { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public long PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Comment { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

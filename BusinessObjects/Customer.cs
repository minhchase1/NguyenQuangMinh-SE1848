using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public required string CompanyName { get; set; } = string.Empty;
        public required string ContactName { get; set; } = string.Empty;
        public required string ContactTitle { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty;
        public required string Phone { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return CustomerID + "\t" + CompanyName + "\t" + ContactName + "\t" + ContactTitle + "\t" + Phone + "\t" + Address;
        }
    }
}

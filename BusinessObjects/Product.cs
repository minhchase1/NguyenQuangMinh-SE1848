using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Product
    {
        public int ProductID { get; set; }
        public required string ProductName { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        
        // Navigation property
        public Category? Category { get; set; }

        public override string ToString()
        {
            return ProductID + "\t" + ProductName + "\t" + CategoryID + "\t" + UnitPrice + "\t" + UnitsInStock;
        }
    }
}

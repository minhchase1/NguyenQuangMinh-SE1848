using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        
        // Navigation properties
        public Product? Product { get; set; }
        
        // Computed property for DataGrid binding
        public decimal Total => GetTotal();
        
        public decimal GetTotal()
        {
            return UnitPrice * Quantity * (1 - Discount);
        }
        
        public override string ToString()
        {
            return OrderID + "\t" + ProductID + "\t" + UnitPrice + "\t" + Quantity + "\t" + Discount;
        }
    }
} 
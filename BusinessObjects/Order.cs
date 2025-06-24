using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        
        // Navigation properties
        public Customer? Customer { get; set; }
        public required Employee Employee { get; set; } = null!;
        public required List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        
        public override string ToString()
        {
            return OrderID + "\t" + CustomerID + "\t" + EmployeeID + "\t" + OrderDate.ToShortDateString();
        }
    }
} 
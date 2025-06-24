using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Category
    {
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return CategoryID + "\t" + CategoryName + "\t" + Description;
        }
    }
} 
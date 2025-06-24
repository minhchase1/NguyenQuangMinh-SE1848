using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string UserName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string JobTitle { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return EmployeeID + "\t" + Name + "\t" + UserName + "\t" + JobTitle;
        }
    }
} 
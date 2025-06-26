using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        private static List<Employee> employees = new List<Employee>();
        private static EmployeeDAO? instance = null;
        private static readonly object instanceLock = new object();

        private EmployeeDAO() { }

        public static EmployeeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            employees.Clear();
            
            // Admin và Management
            employees.Add(new Employee { EmployeeID = 1, Name = "Nguyễn Văn Admin", UserName = "admin", Password = "admin123", JobTitle = "Quản trị hệ thống" });
            employees.Add(new Employee { EmployeeID = 2, Name = "Trần Thị Manager", UserName = "manager", Password = "manager123", JobTitle = "Quản lý bán hàng" });
            
            // Sales Team
            employees.Add(new Employee { EmployeeID = 3, Name = "Lê Văn Hùng", UserName = "hunglv", Password = "123456", JobTitle = "Nhân viên bán hàng" });
            employees.Add(new Employee { EmployeeID = 4, Name = "Phạm Thị Linh", UserName = "linhpt", Password = "123456", JobTitle = "Nhân viên bán hàng" });
            employees.Add(new Employee { EmployeeID = 5, Name = "Hoàng Văn Minh", UserName = "minhhv", Password = "123456", JobTitle = "Nhân viên bán hàng" });
            employees.Add(new Employee { EmployeeID = 6, Name = "Vũ Thị Nga", UserName = "ngavt", Password = "123456", JobTitle = "Nhân viên bán hàng" });
            employees.Add(new Employee { EmployeeID = 7, Name = "Đỗ Văn Phong", UserName = "phongdv", Password = "123456", JobTitle = "Nhân viên bán hàng" });
            
            // Support Team
            employees.Add(new Employee { EmployeeID = 8, Name = "Bùi Thị Quỳnh", UserName = "quynhbt", Password = "123456", JobTitle = "Hỗ trợ khách hàng" });
            employees.Add(new Employee { EmployeeID = 9, Name = "Ngô Văn Rồng", UserName = "rongnv", Password = "123456", JobTitle = "Hỗ trợ kỹ thuật" });
            employees.Add(new Employee { EmployeeID = 10, Name = "Lý Thị Sương", UserName = "suonglt", Password = "123456", JobTitle = "Hỗ trợ khách hàng" });
            
            // Warehouse Team
            employees.Add(new Employee { EmployeeID = 11, Name = "Trịnh Văn Tài", UserName = "taitv", Password = "123456", JobTitle = "Quản lý kho" });
            employees.Add(new Employee { EmployeeID = 12, Name = "Đặng Thị Uyên", UserName = "uyendt", Password = "123456", JobTitle = "Nhân viên kho" });
            employees.Add(new Employee { EmployeeID = 13, Name = "Phan Văn Việt", UserName = "vietpv", Password = "123456", JobTitle = "Nhân viên kho" });
            
            // Accounting Team
            employees.Add(new Employee { EmployeeID = 14, Name = "Cao Thị Xuân", UserName = "xuanct", Password = "123456", JobTitle = "Kế toán" });
            employees.Add(new Employee { EmployeeID = 15, Name = "Lưu Văn Yên", UserName = "yenlv", Password = "123456", JobTitle = "Kế toán trưởng" });
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee? GetEmployeeByID(int employeeID)
        {
            return employees.FirstOrDefault(e => e.EmployeeID == employeeID);
        }

        public Employee? GetEmployeeByUsername(string username)
        {
            return employees.FirstOrDefault(e => e.UserName == username);
        }

        public bool ValidateEmployee(string username, string password)
        {
            return employees.Any(e => e.UserName == username && e.Password == password);
        }

        public void AddEmployee(Employee employee)
        {
            // Auto-generate ID if not provided
            if (employee.EmployeeID <= 0)
            {
                employee.EmployeeID = employees.Count > 0 ? employees.Max(e => e.EmployeeID) + 1 : 1;
            }
            employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = GetEmployeeByID(employee.EmployeeID);
            if (existingEmployee != null)
            {
                int index = employees.IndexOf(existingEmployee);
                employees[index] = employee;
            }
            else
            {
                throw new Exception("Employee not found.");
            }
        }

        public void DeleteEmployee(int employeeID)
        {
            var employee = GetEmployeeByID(employeeID);
            if (employee != null)
            {
                employees.Remove(employee);
            }
            else
            {
                throw new Exception("Employee not found.");
            }
        }
    }
} 
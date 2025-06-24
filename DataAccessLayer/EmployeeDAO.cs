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
            employees.Add(new Employee { EmployeeID = 1, Name = "John Smith", UserName = "john", Password = "pass123", JobTitle = "Sales Manager" });
            employees.Add(new Employee { EmployeeID = 2, Name = "Jane Doe", UserName = "jane", Password = "pass456", JobTitle = "Sales Representative" });
            employees.Add(new Employee { EmployeeID = 3, Name = "Michael Johnson", UserName = "michael", Password = "pass789", JobTitle = "Sales Representative" });
            employees.Add(new Employee { EmployeeID = 4, Name = "Emily Williams", UserName = "emily", Password = "pass012", JobTitle = "Sales Assistant" });
            employees.Add(new Employee { EmployeeID = 5, Name = "David Brown", UserName = "david", Password = "pass345", JobTitle = "Sales Assistant" });
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
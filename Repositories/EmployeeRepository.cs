using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDAO employeeDAO;
        
        public EmployeeRepository()
        {
            employeeDAO = EmployeeDAO.Instance;
        }

        public void GenerateSampleDataset()
        {
            employeeDAO.GenerateSampleDataset();
        }

        public List<Employee> GetEmployees()
        {
            return employeeDAO.GetEmployees();
        }
        
        public Employee GetEmployeeByID(int employeeID)
        {
            return employeeDAO.GetEmployeeByID(employeeID);
        }
        
        public Employee GetEmployeeByUsername(string username)
        {
            return employeeDAO.GetEmployeeByUsername(username);
        }
        
        public bool ValidateEmployee(string username, string password)
        {
            return employeeDAO.ValidateEmployee(username, password);
        }
        
        public void AddEmployee(Employee employee)
        {
            employeeDAO.AddEmployee(employee);
        }
        
        public void UpdateEmployee(Employee employee)
        {
            employeeDAO.UpdateEmployee(employee);
        }
        
        public void DeleteEmployee(int employeeID)
        {
            employeeDAO.DeleteEmployee(employeeID);
        }
    }
} 
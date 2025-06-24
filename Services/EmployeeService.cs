using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository employeeRepository;
        
        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository();
        }
        
        public void GenerateSampleDataset()
        {
            employeeRepository.GenerateSampleDataset();
        }

        public List<Employee> GetEmployees()
        {
            return employeeRepository.GetEmployees();
        }
        
        public Employee GetEmployeeByID(int employeeID)
        {
            return employeeRepository.GetEmployeeByID(employeeID);
        }
        
        public Employee GetEmployeeByUsername(string username)
        {
            return employeeRepository.GetEmployeeByUsername(username);
        }
        
        public bool ValidateEmployee(string username, string password)
        {
            return employeeRepository.ValidateEmployee(username, password);
        }
        
        public void AddEmployee(Employee employee)
        {
            employeeRepository.AddEmployee(employee);
        }
        
        public void UpdateEmployee(Employee employee)
        {
            employeeRepository.UpdateEmployee(employee);
        }
        
        public void DeleteEmployee(int employeeID)
        {
            employeeRepository.DeleteEmployee(employeeID);
        }
    }
} 
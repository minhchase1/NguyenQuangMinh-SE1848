using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        void GenerateSampleDataset();
        List<Employee> GetEmployees();
        Employee GetEmployeeByID(int employeeID);
        Employee GetEmployeeByUsername(string username);
        bool ValidateEmployee(string username, string password);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeID);
    }
} 
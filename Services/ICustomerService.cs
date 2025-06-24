using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        void GenerateSampleDataset();
        List<Customer> GetCustomers();
        Customer GetCustomerByID(int customerID);
        Customer GetCustomerByPhone(string phone);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerID);
        List<Customer> SearchCustomers(string searchTerm);
        bool ValidateCustomer(string phone, string password);
    }
}

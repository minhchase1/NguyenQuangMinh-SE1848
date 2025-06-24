using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDAO customerDAO;
        
        public CustomerRepository()
        {
            customerDAO = CustomerDAO.Instance;
        }
        
        public void GenerateSampleDataset()
        {
            customerDAO.GenerateSampleDataset();
        }

        public List<Customer> GetCustomers()
        {
            return customerDAO.GetCustomers();
        }
        
        public Customer GetCustomerByID(int customerID)
        {
            return customerDAO.GetCustomerByID(customerID);
        }
        
        public Customer GetCustomerByPhone(string phone)
        {
            return customerDAO.GetCustomerByPhone(phone);
        }
        
        public void AddCustomer(Customer customer)
        {
            customerDAO.AddCustomer(customer);
        }
        
        public void UpdateCustomer(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
        }
        
        public void DeleteCustomer(int customerID)
        {
            customerDAO.DeleteCustomer(customerID);
        }
        
        public List<Customer> SearchCustomers(string searchTerm)
        {
            return customerDAO.SearchCustomers(searchTerm);
        }
    }
}

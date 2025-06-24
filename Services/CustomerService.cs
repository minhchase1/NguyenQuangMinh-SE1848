using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        
        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }
        
        public void GenerateSampleDataset()
        {
            customerRepository.GenerateSampleDataset();
        }

        public List<Customer> GetCustomers()
        {
            return customerRepository.GetCustomers();
        }
        
        public Customer GetCustomerByID(int customerID)
        {
            return customerRepository.GetCustomerByID(customerID);
        }
        
        public Customer GetCustomerByPhone(string phone)
        {
            return customerRepository.GetCustomerByPhone(phone);
        }
        
        public void AddCustomer(Customer customer)
        {
            customerRepository.AddCustomer(customer);
        }
        
        public void UpdateCustomer(Customer customer)
        {
            customerRepository.UpdateCustomer(customer);
        }
        
        public void DeleteCustomer(int customerID)
        {
            customerRepository.DeleteCustomer(customerID);
        }
        
        public List<Customer> SearchCustomers(string searchTerm)
        {
            return customerRepository.SearchCustomers(searchTerm);
        }
        
        public bool ValidateCustomer(string phone, string password)
        {
            var customer = customerRepository.GetCustomerByPhone(phone);
            return customer != null && customer.Password == password;
        }
    }
}

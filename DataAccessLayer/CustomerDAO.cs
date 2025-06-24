using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static List<Customer> customers = new List<Customer>();
        private static CustomerDAO? instance = null;
        private static readonly object instanceLock = new object();

        private CustomerDAO() { }

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            customers.Clear();
            customers.Add(new Customer { CustomerID = 1, CompanyName = "ABC Corp", ContactName = "John Doe", ContactTitle = "Manager", Address = "123 Elm St", Phone = "123-456-7890", Password = "password1" });
            customers.Add(new Customer { CustomerID = 2, CompanyName = "XYZ Ltd", ContactName = "Alice Smith", ContactTitle = "Director", Address = "456 Oak St", Phone = "987-654-3210", Password = "password2" });
            customers.Add(new Customer { CustomerID = 3, CompanyName = "123 Industries", ContactName = "Charlie Brown", ContactTitle = "CEO", Address = "789 Pine St", Phone = "555-555-5555", Password = "password3" });
            customers.Add(new Customer { CustomerID = 4, CompanyName = "Tech Solutions", ContactName = "Diana Prince", ContactTitle = "CFO", Address = "321 Maple St", Phone = "111-222-3333", Password = "password4" });
            customers.Add(new Customer { CustomerID = 5, CompanyName = "Global Services", ContactName = "Clark Kent", ContactTitle = "COO", Address = "654 Cedar St", Phone = "444-555-6666", Password = "password5" });
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public Customer? GetCustomerByID(int customerID)
        {
            return customers.FirstOrDefault(c => c.CustomerID == customerID);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            return customers.FirstOrDefault(c => c.Phone == phone);
        }

        public void AddCustomer(Customer customer)
        {
            // Auto-generate ID if not provided
            if (customer.CustomerID <= 0)
            {
                customer.CustomerID = customers.Count > 0 ? customers.Max(c => c.CustomerID) + 1 : 1;
            }
            customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerByID(customer.CustomerID);
            if (existingCustomer != null)
            {
                int index = customers.IndexOf(existingCustomer);
                customers[index] = customer;
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        public void DeleteCustomer(int customerID)
        {
            var customer = GetCustomerByID(customerID);
            if (customer != null)
            {
                customers.Remove(customer);
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return customers.Where(c => 
                c.CompanyName.ToLower().Contains(searchTerm) || 
                c.ContactName.ToLower().Contains(searchTerm) || 
                c.ContactTitle.ToLower().Contains(searchTerm) || 
                c.Address.ToLower().Contains(searchTerm) || 
                c.Phone.Contains(searchTerm)
            ).ToList();
        }
    }
}

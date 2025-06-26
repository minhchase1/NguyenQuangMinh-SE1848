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
            
            // Thêm nhiều khách hàng ảo hơn
            customers.Add(new Customer { CustomerID = 1, CompanyName = "ABC Technology Corp", ContactName = "Nguyễn Văn An", ContactTitle = "Giám đốc", Address = "123 Lê Lợi, Q1, TP.HCM", Phone = "0901234567", Password = "123456" });
            customers.Add(new Customer { CustomerID = 2, CompanyName = "XYZ Trading Ltd", ContactName = "Trần Thị Bình", ContactTitle = "Trưởng phòng kinh doanh", Address = "456 Nguyễn Huệ, Q1, TP.HCM", Phone = "0912345678", Password = "123456" });
            customers.Add(new Customer { CustomerID = 3, CompanyName = "DEF Solutions", ContactName = "Lê Văn Cường", ContactTitle = "CEO", Address = "789 Điện Biên Phủ, Q3, TP.HCM", Phone = "0923456789", Password = "123456" });
            customers.Add(new Customer { CustomerID = 4, CompanyName = "GHI Electronics", ContactName = "Phạm Thị Dung", ContactTitle = "CFO", Address = "321 Võ Văn Tần, Q3, TP.HCM", Phone = "0934567890", Password = "123456" });
            customers.Add(new Customer { CustomerID = 5, CompanyName = "JKL Services", ContactName = "Hoàng Văn Em", ContactTitle = "COO", Address = "654 Pasteur, Q1, TP.HCM", Phone = "0945678901", Password = "123456" });
            customers.Add(new Customer { CustomerID = 6, CompanyName = "MNO Import Export", ContactName = "Vũ Thị Phương", ContactTitle = "Giám đốc", Address = "987 Hai Bà Trưng, Q1, TP.HCM", Phone = "0956789012", Password = "123456" });
            customers.Add(new Customer { CustomerID = 7, CompanyName = "PQR Manufacturing", ContactName = "Đỗ Văn Giang", ContactTitle = "Phó giám đốc", Address = "147 Cách Mạng Tháng 8, Q10, TP.HCM", Phone = "0967890123", Password = "123456" });
            customers.Add(new Customer { CustomerID = 8, CompanyName = "STU Retail", ContactName = "Bùi Thị Hạnh", ContactTitle = "Quản lý", Address = "258 Lý Tự Trọng, Q1, TP.HCM", Phone = "0978901234", Password = "123456" });
            customers.Add(new Customer { CustomerID = 9, CompanyName = "VWX Logistics", ContactName = "Ngô Văn Ích", ContactTitle = "Trưởng phòng", Address = "369 Trần Hưng Đạo, Q5, TP.HCM", Phone = "0989012345", Password = "123456" });
            customers.Add(new Customer { CustomerID = 10, CompanyName = "YZ Consulting", ContactName = "Lý Thị Kim", ContactTitle = "Chuyên viên", Address = "741 Nguyễn Thị Minh Khai, Q3, TP.HCM", Phone = "0990123456", Password = "123456" });
            customers.Add(new Customer { CustomerID = 11, CompanyName = "Alpha Software", ContactName = "Trịnh Văn Long", ContactTitle = "Technical Lead", Address = "852 Võ Thị Sáu, Q3, TP.HCM", Phone = "0901234560", Password = "123456" });
            customers.Add(new Customer { CustomerID = 12, CompanyName = "Beta Hardware", ContactName = "Đặng Thị Mai", ContactTitle = "Sales Manager", Address = "963 Nguyễn Đình Chiểu, Q3, TP.HCM", Phone = "0912345601", Password = "123456" });
            customers.Add(new Customer { CustomerID = 13, CompanyName = "Gamma Networks", ContactName = "Phan Văn Nam", ContactTitle = "Network Admin", Address = "159 Lê Văn Sỹ, Q3, TP.HCM", Phone = "0923456012", Password = "123456" });
            customers.Add(new Customer { CustomerID = 14, CompanyName = "Delta Systems", ContactName = "Cao Thị Oanh", ContactTitle = "System Analyst", Address = "357 Cộng Hòa, Tân Bình, TP.HCM", Phone = "0934560123", Password = "123456" });
            customers.Add(new Customer { CustomerID = 15, CompanyName = "Epsilon Mobile", ContactName = "Lưu Văn Phúc", ContactTitle = "Mobile Developer", Address = "468 Hoàng Văn Thụ, Tân Bình, TP.HCM", Phone = "0945601234", Password = "123456" });
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

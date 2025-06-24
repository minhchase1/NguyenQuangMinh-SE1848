using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDAO
    {
        private static List<Order> orders = new List<Order>();
        private static OrderDAO? instance = null;
        private static readonly object instanceLock = new object();
        
        private CustomerDAO customerDAO = CustomerDAO.Instance;
        private EmployeeDAO employeeDAO = EmployeeDAO.Instance;

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            // First, make sure we have employees to reference
            employeeDAO.GenerateSampleDataset();
            customerDAO.GenerateSampleDataset();
            
            orders.Clear();
            
            // Create orders with properly initialized Employee objects
            for (int i = 1; i <= 5; i++)
            {
                var employee = employeeDAO.GetEmployeeByID(i);
                if (employee != null)
                {
                    orders.Add(new Order { 
                        OrderID = i, 
                        CustomerID = i, 
                        EmployeeID = i, 
                        OrderDate = DateTime.Now.AddDays(-i), 
                        Employee = employee,
                        OrderDetails = new List<OrderDetail>() 
                    });
                }
            }
            
            // Set navigation properties for customers
            foreach (var order in orders)
            {
                order.Customer = customerDAO.GetCustomerByID(order.CustomerID);
            }
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public Order? GetOrderByID(int orderID)
        {
            return orders.FirstOrDefault(o => o.OrderID == orderID);
        }

        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            return orders.Where(o => o.CustomerID == customerID).ToList();
        }

        public List<Order> GetOrdersByEmployeeID(int employeeID)
        {
            return orders.Where(o => o.EmployeeID == employeeID).ToList();
        }

        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
        }

        public void AddOrder(Order order)
        {
            // Auto-generate ID if not provided
            if (order.OrderID <= 0)
            {
                order.OrderID = orders.Count > 0 ? orders.Max(o => o.OrderID) + 1 : 1;
            }
            
            // Make sure Employee is set
            if (order.Employee == null)
            {
                var employee = employeeDAO.GetEmployeeByID(order.EmployeeID);
                if (employee == null)
                {
                    throw new Exception("Employee not found.");
                }
                order.Employee = employee;
            }
            
            // Set navigation properties
            order.Customer = customerDAO.GetCustomerByID(order.CustomerID);
            
            orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = GetOrderByID(order.OrderID);
            if (existingOrder != null)
            {
                int index = orders.IndexOf(existingOrder);
                
                // Make sure Employee is set
                if (order.Employee == null)
                {
                    var employee = employeeDAO.GetEmployeeByID(order.EmployeeID);
                    if (employee == null)
                    {
                        throw new Exception("Employee not found.");
                    }
                    order.Employee = employee;
                }
                
                // Set navigation properties
                order.Customer = customerDAO.GetCustomerByID(order.CustomerID);
                
                orders[index] = order;
            }
            else
            {
                throw new Exception("Order not found.");
            }
        }

        public void DeleteOrder(int orderID)
        {
            var order = GetOrderByID(orderID);
            if (order != null)
            {
                orders.Remove(order);
            }
            else
            {
                throw new Exception("Order not found.");
            }
        }
    }
} 
using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;
        
        public OrderService()
        {
            orderRepository = new OrderRepository();
        }
        
        public void GenerateSampleDataset()
        {
            orderRepository.GenerateSampleDataset();
        }

        public List<Order> GetOrders()
        {
            return orderRepository.GetOrders();
        }
        
        public Order GetOrderByID(int orderID)
        {
            return orderRepository.GetOrderByID(orderID);
        }
        
        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            return orderRepository.GetOrdersByCustomerID(customerID);
        }
        
        public List<Order> GetOrdersByEmployeeID(int employeeID)
        {
            return orderRepository.GetOrdersByEmployeeID(employeeID);
        }
        
        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return orderRepository.GetOrdersByDateRange(startDate, endDate);
        }
        
        public void AddOrder(Order order)
        {
            orderRepository.AddOrder(order);
        }
        
        public void UpdateOrder(Order order)
        {
            orderRepository.UpdateOrder(order);
        }
        
        public void DeleteOrder(int orderID)
        {
            orderRepository.DeleteOrder(orderID);
        }
    }
} 
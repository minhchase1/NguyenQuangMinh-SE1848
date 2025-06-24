using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDAO orderDAO;
        private OrderDetailDAO orderDetailDAO;
        
        public OrderRepository()
        {
            orderDAO = OrderDAO.Instance;
            orderDetailDAO = OrderDetailDAO.Instance;
        }

        public void GenerateSampleDataset()
        {
            orderDAO.GenerateSampleDataset();
            orderDetailDAO.GenerateSampleDataset();
            
            // Set OrderDetails for each order
            var orders = orderDAO.GetOrders();
            foreach (var order in orders)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
            }
        }

        public List<Order> GetOrders()
        {
            var orders = orderDAO.GetOrders();
            foreach (var order in orders)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
            }
            return orders;
        }
        
        public Order GetOrderByID(int orderID)
        {
            var order = orderDAO.GetOrderByID(orderID);
            if (order != null)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(orderID);
            }
            return order;
        }
        
        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            var orders = orderDAO.GetOrdersByCustomerID(customerID);
            foreach (var order in orders)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
            }
            return orders;
        }
        
        public List<Order> GetOrdersByEmployeeID(int employeeID)
        {
            var orders = orderDAO.GetOrdersByEmployeeID(employeeID);
            foreach (var order in orders)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
            }
            return orders;
        }
        
        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = orderDAO.GetOrdersByDateRange(startDate, endDate);
            foreach (var order in orders)
            {
                order.OrderDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
            }
            return orders;
        }
        
        public void AddOrder(Order order)
        {
            orderDAO.AddOrder(order);
            
            if (order.OrderDetails != null)
            {
                foreach (var detail in order.OrderDetails)
                {
                    detail.OrderID = order.OrderID;
                    orderDetailDAO.AddOrderDetail(detail);
                }
            }
        }
        
        public void UpdateOrder(Order order)
        {
            orderDAO.UpdateOrder(order);
            
            // Handle order details updates if needed
            if (order.OrderDetails != null)
            {
                var existingDetails = orderDetailDAO.GetOrderDetailsByOrderID(order.OrderID);
                
                // Delete details that are no longer present
                foreach (var existingDetail in existingDetails)
                {
                    if (!order.OrderDetails.Any(d => d.ProductID == existingDetail.ProductID))
                    {
                        orderDetailDAO.DeleteOrderDetail(order.OrderID, existingDetail.ProductID);
                    }
                }
                
                // Add or update details
                foreach (var detail in order.OrderDetails)
                {
                    detail.OrderID = order.OrderID;
                    var existingDetail = existingDetails.FirstOrDefault(d => d.ProductID == detail.ProductID);
                    
                    if (existingDetail == null)
                    {
                        // New detail
                        orderDetailDAO.AddOrderDetail(detail);
                    }
                    else
                    {
                        // Update existing detail
                        orderDetailDAO.UpdateOrderDetail(detail);
                    }
                }
            }
        }
        
        public void DeleteOrder(int orderID)
        {
            // Delete associated order details first
            var details = orderDetailDAO.GetOrderDetailsByOrderID(orderID);
            foreach (var detail in details)
            {
                orderDetailDAO.DeleteOrderDetail(orderID, detail.ProductID);
            }
            
            // Then delete the order
            orderDAO.DeleteOrder(orderID);
        }
    }
} 
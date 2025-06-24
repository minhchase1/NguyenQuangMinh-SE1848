using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        void GenerateSampleDataset();
        List<Order> GetOrders();
        Order GetOrderByID(int orderID);
        List<Order> GetOrdersByCustomerID(int customerID);
        List<Order> GetOrdersByEmployeeID(int employeeID);
        List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderID);
    }
} 
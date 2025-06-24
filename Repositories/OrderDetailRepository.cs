using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private OrderDetailDAO orderDetailDAO;
        
        public OrderDetailRepository()
        {
            orderDetailDAO = OrderDetailDAO.Instance;
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            return orderDetailDAO.GetOrderDetailsByOrderID(orderID);
        }
        
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            orderDetailDAO.AddOrderDetail(orderDetail);
        }
        
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            orderDetailDAO.UpdateOrderDetail(orderDetail);
        }
        
        public void DeleteOrderDetail(int orderID, int productID)
        {
            orderDetailDAO.DeleteOrderDetail(orderID, productID);
        }
    }
} 
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrderDetailDAO
    {
        private static List<OrderDetail> orderDetails = new List<OrderDetail>();
        private static OrderDetailDAO? instance = null;
        private static readonly object instanceLock = new object();
        
        private ProductDAO productDAO = ProductDAO.Instance;

        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            // Make sure product data is loaded
            productDAO.GenerateSampleDataset();
            
            orderDetails.Clear();
            orderDetails.Add(new OrderDetail { OrderID = 1, ProductID = 1, UnitPrice = 999.99m, Quantity = 1, Discount = 0.05f });
            orderDetails.Add(new OrderDetail { OrderID = 1, ProductID = 5, UnitPrice = 99.99m, Quantity = 2, Discount = 0f });
            orderDetails.Add(new OrderDetail { OrderID = 2, ProductID = 2, UnitPrice = 799.99m, Quantity = 1, Discount = 0.1f });
            orderDetails.Add(new OrderDetail { OrderID = 3, ProductID = 3, UnitPrice = 299.99m, Quantity = 2, Discount = 0f });
            orderDetails.Add(new OrderDetail { OrderID = 4, ProductID = 4, UnitPrice = 699.99m, Quantity = 1, Discount = 0.05f });
            orderDetails.Add(new OrderDetail { OrderID = 5, ProductID = 5, UnitPrice = 99.99m, Quantity = 3, Discount = 0.15f });
            
            // Set navigation properties
            foreach (var detail in orderDetails)
            {
                detail.Product = productDAO.GetProductByID(detail.ProductID);
            }
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int orderID)
        {
            return orderDetails.Where(od => od.OrderID == orderID).ToList();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            // Set navigation property
            orderDetail.Product = productDAO.GetProductByID(orderDetail.ProductID);
            
            orderDetails.Add(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            var existingOrderDetail = orderDetails.FirstOrDefault(od => 
                od.OrderID == orderDetail.OrderID && od.ProductID == orderDetail.ProductID);
                
            if (existingOrderDetail != null)
            {
                int index = orderDetails.IndexOf(existingOrderDetail);
                
                // Set navigation property
                orderDetail.Product = productDAO.GetProductByID(orderDetail.ProductID);
                
                orderDetails[index] = orderDetail;
            }
            else
            {
                throw new Exception("Order detail not found.");
            }
        }

        public void DeleteOrderDetail(int orderID, int productID)
        {
            var orderDetail = orderDetails.FirstOrDefault(od => 
                od.OrderID == orderID && od.ProductID == productID);
                
            if (orderDetail != null)
            {
                orderDetails.Remove(orderDetail);
            }
            else
            {
                throw new Exception("Order detail not found.");
            }
        }
    }
} 
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
            var random = new Random();
            
            // Tạo chi tiết đơn hàng cho 25 orders
            for (int orderID = 1; orderID <= 25; orderID++)
            {
                // Mỗi đơn hàng có 1-4 sản phẩm
                int numProducts = random.Next(1, 5);
                var usedProducts = new HashSet<int>();
                
                for (int j = 0; j < numProducts; j++)
                {
                    int productID;
                    do
                    {
                        productID = random.Next(1, 29); // Products 1-28
                    } while (usedProducts.Contains(productID));
                    
                    usedProducts.Add(productID);
                    
                    var product = productDAO.GetProductByID(productID);
                    if (product != null)
                    {
                        var quantity = random.Next(1, 6);
                        var discount = (float)(random.NextDouble() * 0.2); // 0-20% discount
                        
                        orderDetails.Add(new OrderDetail 
                        { 
                            OrderID = orderID, 
                            ProductID = productID, 
                            UnitPrice = product.UnitPrice, 
                            Quantity = quantity, 
                            Discount = discount,
                            Product = product
                        });
                    }
                }
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
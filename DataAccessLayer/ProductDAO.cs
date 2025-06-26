using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static List<Product> products = new List<Product>();
        private static ProductDAO? instance = null;
        private static readonly object instanceLock = new object();

        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            products.Clear();
            
            // Computers (CategoryID = 1)
            products.Add(new Product { ProductID = 1, ProductName = "Dell Inspiron 15 3000", CategoryID = 1, UnitPrice = 12999000m, UnitsInStock = 25 });
            products.Add(new Product { ProductID = 2, ProductName = "HP Pavilion Desktop", CategoryID = 1, UnitPrice = 15999000m, UnitsInStock = 18 });
            products.Add(new Product { ProductID = 3, ProductName = "Asus VivoBook 14", CategoryID = 1, UnitPrice = 13999000m, UnitsInStock = 30 });
            products.Add(new Product { ProductID = 4, ProductName = "Lenovo ThinkPad E14", CategoryID = 1, UnitPrice = 18999000m, UnitsInStock = 22 });
            products.Add(new Product { ProductID = 5, ProductName = "Acer Aspire 5", CategoryID = 1, UnitPrice = 11999000m, UnitsInStock = 35 });
            products.Add(new Product { ProductID = 6, ProductName = "MacBook Air M1", CategoryID = 1, UnitPrice = 28999000m, UnitsInStock = 15 });
            
            // Mobile Devices (CategoryID = 2)
            products.Add(new Product { ProductID = 7, ProductName = "iPhone 14", CategoryID = 2, UnitPrice = 24999000m, UnitsInStock = 40 });
            products.Add(new Product { ProductID = 8, ProductName = "Samsung Galaxy S23", CategoryID = 2, UnitPrice = 22999000m, UnitsInStock = 35 });
            products.Add(new Product { ProductID = 9, ProductName = "iPad Air", CategoryID = 2, UnitPrice = 16999000m, UnitsInStock = 28 });
            products.Add(new Product { ProductID = 10, ProductName = "Xiaomi Redmi Note 12", CategoryID = 2, UnitPrice = 5999000m, UnitsInStock = 50 });
            products.Add(new Product { ProductID = 11, ProductName = "Oppo Find X5", CategoryID = 2, UnitPrice = 18999000m, UnitsInStock = 32 });
            products.Add(new Product { ProductID = 12, ProductName = "Samsung Galaxy Tab S8", CategoryID = 2, UnitPrice = 19999000m, UnitsInStock = 20 });
            
            // Accessories (CategoryID = 3)
            products.Add(new Product { ProductID = 13, ProductName = "Sony WH-1000XM4 Headphones", CategoryID = 3, UnitPrice = 7999000m, UnitsInStock = 45 });
            products.Add(new Product { ProductID = 14, ProductName = "Logitech MX Master 3", CategoryID = 3, UnitPrice = 2499000m, UnitsInStock = 60 });
            products.Add(new Product { ProductID = 15, ProductName = "Apple Magic Keyboard", CategoryID = 3, UnitPrice = 4999000m, UnitsInStock = 38 });
            products.Add(new Product { ProductID = 16, ProductName = "Dell UltraSharp Monitor 24\"", CategoryID = 3, UnitPrice = 6999000m, UnitsInStock = 25 });
            products.Add(new Product { ProductID = 17, ProductName = "Razer DeathAdder V3", CategoryID = 3, UnitPrice = 1999000m, UnitsInStock = 55 });
            products.Add(new Product { ProductID = 18, ProductName = "SteelSeries Arctis 7", CategoryID = 3, UnitPrice = 4999000m, UnitsInStock = 42 });
            
            // Software (CategoryID = 4)
            products.Add(new Product { ProductID = 19, ProductName = "Microsoft Office 365", CategoryID = 4, UnitPrice = 1699000m, UnitsInStock = 100 });
            products.Add(new Product { ProductID = 20, ProductName = "Adobe Creative Suite", CategoryID = 4, UnitPrice = 5999000m, UnitsInStock = 75 });
            products.Add(new Product { ProductID = 21, ProductName = "Windows 11 Pro", CategoryID = 4, UnitPrice = 4999000m, UnitsInStock = 80 });
            products.Add(new Product { ProductID = 22, ProductName = "Antivirus Kaspersky", CategoryID = 4, UnitPrice = 899000m, UnitsInStock = 120 });
            products.Add(new Product { ProductID = 23, ProductName = "AutoCAD 2024", CategoryID = 4, UnitPrice = 12999000m, UnitsInStock = 30 });
            
            // Networking (CategoryID = 5)
            products.Add(new Product { ProductID = 24, ProductName = "TP-Link Archer AX73", CategoryID = 5, UnitPrice = 3999000m, UnitsInStock = 35 });
            products.Add(new Product { ProductID = 25, ProductName = "Cisco Catalyst Switch", CategoryID = 5, UnitPrice = 15999000m, UnitsInStock = 12 });
            products.Add(new Product { ProductID = 26, ProductName = "Netgear Nighthawk Router", CategoryID = 5, UnitPrice = 5999000m, UnitsInStock = 28 });
            products.Add(new Product { ProductID = 27, ProductName = "Ubiquiti UniFi AP", CategoryID = 5, UnitPrice = 4499000m, UnitsInStock = 40 });
            products.Add(new Product { ProductID = 28, ProductName = "D-Link Managed Switch", CategoryID = 5, UnitPrice = 7999000m, UnitsInStock = 18 });
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product? GetProductByID(int productID)
        {
            return products.FirstOrDefault(p => p.ProductID == productID);
        }

        public void AddProduct(Product product)
        {
            // Auto-generate ID if not provided
            if (product.ProductID <= 0)
            {
                product.ProductID = products.Count > 0 ? products.Max(p => p.ProductID) + 1 : 1;
            }
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = GetProductByID(product.ProductID);
            if (existingProduct != null)
            {
                int index = products.IndexOf(existingProduct);
                products[index] = product;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public void DeleteProduct(int productID)
        {
            var product = GetProductByID(productID);
            if (product != null)
            {
                products.Remove(product);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return products.Where(p => 
                p.ProductName.ToLower().Contains(searchTerm)
            ).ToList();
        }

        public List<Product> GetProductsByCategory(int categoryID)
        {
            return products.Where(p => p.CategoryID == categoryID).ToList();
        }
    }
} 
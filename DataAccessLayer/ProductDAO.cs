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
            products.Add(new Product { ProductID = 1, ProductName = "Laptop", CategoryID = 1, UnitPrice = 999.99m, UnitsInStock = 20 });
            products.Add(new Product { ProductID = 2, ProductName = "Desktop", CategoryID = 1, UnitPrice = 799.99m, UnitsInStock = 15 });
            products.Add(new Product { ProductID = 3, ProductName = "Tablet", CategoryID = 1, UnitPrice = 299.99m, UnitsInStock = 30 });
            products.Add(new Product { ProductID = 4, ProductName = "Smartphone", CategoryID = 2, UnitPrice = 699.99m, UnitsInStock = 25 });
            products.Add(new Product { ProductID = 5, ProductName = "Headphones", CategoryID = 3, UnitPrice = 99.99m, UnitsInStock = 50 });
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
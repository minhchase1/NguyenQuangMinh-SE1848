using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductDAO productDAO;
        
        public ProductRepository()
        {
            productDAO = ProductDAO.Instance;
        }

        public void GenerateSampleDataset()
        {
            productDAO.GenerateSampleDataset();
        }

        public List<Product> GetProducts()
        {
            return productDAO.GetProducts();
        }
        
        public Product GetProductByID(int productID)
        {
            return productDAO.GetProductByID(productID);
        }
        
        public void AddProduct(Product product)
        {
            productDAO.AddProduct(product);
        }
        
        public void UpdateProduct(Product product)
        {
            productDAO.UpdateProduct(product);
        }
        
        public void DeleteProduct(int productID)
        {
            productDAO.DeleteProduct(productID);
        }
        
        public List<Product> SearchProducts(string searchTerm)
        {
            return productDAO.SearchProducts(searchTerm);
        }
        
        public List<Product> GetProductsByCategory(int categoryID)
        {
            return productDAO.GetProductsByCategory(categoryID);
        }
    }
} 
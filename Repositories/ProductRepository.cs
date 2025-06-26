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
            var products = productDAO.GetProducts();
            
            // Populate Category navigation property
            var categoryRepository = new CategoryRepository();
            foreach (var product in products)
            {
                product.Category = categoryRepository.GetCategoryByID(product.CategoryID);
            }
            
            return products;
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
            var products = productDAO.SearchProducts(searchTerm);
            
            // Populate Category navigation property
            var categoryRepository = new CategoryRepository();
            foreach (var product in products)
            {
                product.Category = categoryRepository.GetCategoryByID(product.CategoryID);
            }
            
            return products;
        }
        
        public List<Product> GetProductsByCategory(int categoryID)
        {
            var products = productDAO.GetProductsByCategory(categoryID);
            
            // Populate Category navigation property
            var categoryRepository = new CategoryRepository();
            foreach (var product in products)
            {
                product.Category = categoryRepository.GetCategoryByID(product.CategoryID);
            }
            
            return products;
        }
    }
} 
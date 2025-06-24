using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        
        public ProductService()
        {
            productRepository = new ProductRepository();
        }
        
        public void GenerateSampleDataset()
        {
            productRepository.GenerateSampleDataset();
        }

        public List<Product> GetProducts()
        {
            return productRepository.GetProducts();
        }
        
        public Product GetProductByID(int productID)
        {
            return productRepository.GetProductByID(productID);
        }
        
        public void AddProduct(Product product)
        {
            productRepository.AddProduct(product);
        }
        
        public void UpdateProduct(Product product)
        {
            productRepository.UpdateProduct(product);
        }
        
        public void DeleteProduct(int productID)
        {
            productRepository.DeleteProduct(productID);
        }
        
        public List<Product> SearchProducts(string searchTerm)
        {
            return productRepository.SearchProducts(searchTerm);
        }
        
        public List<Product> GetProductsByCategory(int categoryID)
        {
            return productRepository.GetProductsByCategory(categoryID);
        }
    }
} 
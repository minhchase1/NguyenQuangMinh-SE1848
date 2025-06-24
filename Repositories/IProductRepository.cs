using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        void GenerateSampleDataset();
        List<Product> GetProducts();
        Product GetProductByID(int productID);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productID);
        List<Product> SearchProducts(string searchTerm);
        List<Product> GetProductsByCategory(int categoryID);
    }
} 
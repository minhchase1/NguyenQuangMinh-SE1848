using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryDAO categoryDAO;
        
        public CategoryRepository()
        {
            categoryDAO = CategoryDAO.Instance;
        }

        public void GenerateSampleDataset()
        {
            categoryDAO.GenerateSampleDataset();
        }

        public List<Category> GetCategories()
        {
            return categoryDAO.GetCategories();
        }
        
        public Category GetCategoryByID(int categoryID)
        {
            return categoryDAO.GetCategoryByID(categoryID);
        }
        
        public void AddCategory(Category category)
        {
            categoryDAO.AddCategory(category);
        }
        
        public void UpdateCategory(Category category)
        {
            categoryDAO.UpdateCategory(category);
        }
        
        public void DeleteCategory(int categoryID)
        {
            categoryDAO.DeleteCategory(categoryID);
        }
    }
} 
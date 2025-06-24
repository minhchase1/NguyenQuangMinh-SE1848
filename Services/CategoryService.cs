using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        
        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }
        
        public void GenerateSampleDataset()
        {
            categoryRepository.GenerateSampleDataset();
        }

        public List<Category> GetCategories()
        {
            return categoryRepository.GetCategories();
        }
        
        public Category GetCategoryByID(int categoryID)
        {
            return categoryRepository.GetCategoryByID(categoryID);
        }
        
        public void AddCategory(Category category)
        {
            categoryRepository.AddCategory(category);
        }
        
        public void UpdateCategory(Category category)
        {
            categoryRepository.UpdateCategory(category);
        }
        
        public void DeleteCategory(int categoryID)
        {
            categoryRepository.DeleteCategory(categoryID);
        }
    }
} 
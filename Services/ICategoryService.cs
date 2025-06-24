using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        void GenerateSampleDataset();
        List<Category> GetCategories();
        Category GetCategoryByID(int categoryID);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryID);
    }
} 
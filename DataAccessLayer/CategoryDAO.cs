using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        private static List<Category> categories = new List<Category>();
        private static CategoryDAO? instance = null;
        private static readonly object instanceLock = new object();

        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public void GenerateSampleDataset()
        {
            categories.Clear();
            categories.Add(new Category { CategoryID = 1, CategoryName = "Computers", Description = "Desktop and laptop computers" });
            categories.Add(new Category { CategoryID = 2, CategoryName = "Mobile Devices", Description = "Phones and tablets" });
            categories.Add(new Category { CategoryID = 3, CategoryName = "Accessories", Description = "Computer and mobile accessories" });
            categories.Add(new Category { CategoryID = 4, CategoryName = "Software", Description = "Applications and games" });
            categories.Add(new Category { CategoryID = 5, CategoryName = "Networking", Description = "Routers, switches and networking equipment" });
        }

        public List<Category> GetCategories()
        {
            return categories;
        }

        public Category? GetCategoryByID(int categoryID)
        {
            return categories.FirstOrDefault(c => c.CategoryID == categoryID);
        }

        public void AddCategory(Category category)
        {
            // Auto-generate ID if not provided
            if (category.CategoryID <= 0)
            {
                category.CategoryID = categories.Count > 0 ? categories.Max(c => c.CategoryID) + 1 : 1;
            }
            categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryByID(category.CategoryID);
            if (existingCategory != null)
            {
                int index = categories.IndexOf(existingCategory);
                categories[index] = category;
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }

        public void DeleteCategory(int categoryID)
        {
            var category = GetCategoryByID(categoryID);
            if (category != null)
            {
                categories.Remove(category);
            }
            else
            {
                throw new Exception("Category not found.");
            }
        }
    }
} 
using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;
using System.Collections.Generic;

namespace minhnqWPF.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private ObservableCollection<Product> _products = new();
        private ObservableCollection<Category> _categories = new();
        private Product? _selectedProduct;
        private Category? _selectedCategory;
        private string _searchText = string.Empty;
        private bool _isEditing;
        
        public ProductViewModel()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();
            LoadProducts();
            LoadCategories();
            
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteCancel);
            SearchCommand = new RelayCommand(ExecuteSearch);
            FilterByCategoryCommand = new RelayCommand(ExecuteFilterByCategory);
        }
        
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }
        
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set 
            {
                if (SetProperty(ref _selectedProduct, value))
                {
                    // Refresh command availability
                    ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set 
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    // Auto-filter when category selection changes
                    ExecuteFilterByCategory(null!);
                }
            }
        }
        
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (SetProperty(ref _isEditing, value))
                {
                    // Refresh command availability
                    ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)CancelCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand FilterByCategoryCommand { get; }
        
        private void LoadProducts()
        {
            var productList = _productService.GetProducts();
            Products = new ObservableCollection<Product>(productList);
        }
        
        private void LoadCategories()
        {
            var categoryList = _categoryService.GetCategories();
            
            // Add "All Categories" option at the beginning
            var allCategories = new List<Category>();
            allCategories.Add(new Category { CategoryID = 0, CategoryName = "All Categories", Description = "Show all products" });
            allCategories.AddRange(categoryList);
            
            Categories = new ObservableCollection<Category>(allCategories);
        }
        
        private void ExecuteAdd(object parameter)
        {
            SelectedProduct = new Product
            {
                ProductName = string.Empty
            };
            IsEditing = true;
        }
        
        private bool CanExecuteEdit(object parameter)
        {
            return SelectedProduct != null && !IsEditing;
        }
        
        private void ExecuteEdit(object parameter)
        {
            if (SelectedProduct != null)
            {
                // Create a copy for editing
                SelectedProduct = new Product
                {
                    ProductID = SelectedProduct.ProductID,
                    ProductName = SelectedProduct.ProductName,
                    CategoryID = SelectedProduct.CategoryID,
                    UnitPrice = SelectedProduct.UnitPrice,
                    UnitsInStock = SelectedProduct.UnitsInStock
                };
                IsEditing = true;
            }
        }
        
        private bool CanExecuteDelete(object parameter)
        {
            return SelectedProduct != null && !IsEditing;
        }
        
        private void ExecuteDelete(object parameter)
        {
            if (SelectedProduct != null)
            {
                _productService.DeleteProduct(SelectedProduct.ProductID);
                Products.Remove(SelectedProduct);
                SelectedProduct = null;
            }
        }
        
        private bool CanExecuteSave(object parameter)
        {
            return IsEditing && SelectedProduct != null &&
                   !string.IsNullOrEmpty(SelectedProduct.ProductName) &&
                   SelectedProduct.UnitPrice >= 0;
        }
        
        private void ExecuteSave(object parameter)
        {
            if (SelectedProduct != null)
            {
                if (SelectedProduct.ProductID == 0)
                {
                    // New product
                    _productService.AddProduct(SelectedProduct);
                    Products.Add(SelectedProduct);
                }
                else
                {
                    // Existing product
                    _productService.UpdateProduct(SelectedProduct);
                    
                    // Refresh the list
                    LoadProducts();
                }
                
                IsEditing = false;
            }
        }
        
        private bool CanExecuteCancel(object parameter)
        {
            return IsEditing;
        }
        
        private void ExecuteCancel(object parameter)
        {
            if (SelectedProduct != null && SelectedProduct.ProductID == 0)
            {
                // If we were adding a new product, clear selection
                SelectedProduct = null;
            }
            
            IsEditing = false;
            LoadProducts(); // Reload to get original data
        }
        
        private void ExecuteSearch(object parameter)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                LoadProducts();
            }
            else
            {
                var results = _productService.SearchProducts(SearchText);
                Products = new ObservableCollection<Product>(results);
            }
        }
        
        private void ExecuteFilterByCategory(object parameter)
        {
            if (SelectedCategory != null && SelectedCategory.CategoryID > 0)
            {
                var results = _productService.GetProductsByCategory(SelectedCategory.CategoryID);
                Products = new ObservableCollection<Product>(results);
            }
            else
            {
                // Show all products if no category selected or "All Categories" selected
                LoadProducts();
            }
        }
    }
} 
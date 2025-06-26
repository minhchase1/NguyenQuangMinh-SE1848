using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace minhnqWPF.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IEmployeeService _employeeService;
        private ObservableCollection<Order> _orders = new();
        private Order? _selectedOrder;
        private bool _isEditing;
        
        // Collections for dropdowns
        private ObservableCollection<Customer> _customers = new();
        private ObservableCollection<Employee> _employees = new();
        private ObservableCollection<Product> _products = new();
        private ObservableCollection<OrderDetail> _orderDetails = new();
        
        // Selected items for order creation
        private Customer? _selectedCustomer;
        private Employee? _selectedEmployee;
        private Product? _selectedProduct;
        private OrderDetail? _selectedOrderDetail;
        
        // Order detail fields
        private int _quantity = 1;
        private decimal _discount = 0;
        
        public OrderViewModel()
        {
            _orderService = new OrderService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _employeeService = new EmployeeService();
            
            // Data is already initialized in App.xaml.cs
            
            LoadOrders();
            LoadCustomers();
            LoadEmployees();
            LoadProducts();
            
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteCancel);
            FilterByDateCommand = new RelayCommand(ExecuteFilterByDate);
            AddProductToOrderCommand = new RelayCommand(ExecuteAddProductToOrder, CanExecuteAddProductToOrder);
            RemoveProductFromOrderCommand = new RelayCommand(ExecuteRemoveProductFromOrder, CanExecuteRemoveProductFromOrder);
        }
        
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set 
            {
                if (SetProperty(ref _selectedOrder, value))
                {
                    // Refresh command availability
                    ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                }
            }
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
        public ICommand FilterByDateCommand { get; }
        public ICommand AddProductToOrderCommand { get; }
        public ICommand RemoveProductFromOrderCommand { get; }
        
        // Properties for collections
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }
        
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }
        
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        
        public ObservableCollection<OrderDetail> OrderDetails
        {
            get => _orderDetails;
            set => SetProperty(ref _orderDetails, value);
        }
        
        // Properties for selected items
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }
        
        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set => SetProperty(ref _selectedEmployee, value);
        }
        
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set 
            {
                if (SetProperty(ref _selectedProduct, value))
                {
                    ((RelayCommand)AddProductToOrderCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public OrderDetail? SelectedOrderDetail
        {
            get => _selectedOrderDetail;
            set 
            {
                if (SetProperty(ref _selectedOrderDetail, value))
                {
                    ((RelayCommand)RemoveProductFromOrderCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public int Quantity
        {
            get => _quantity;
            set 
            {
                if (SetProperty(ref _quantity, value))
                {
                    ((RelayCommand)AddProductToOrderCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public decimal Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }
        
        private void LoadOrders()
        {
            var orderList = _orderService.GetOrders();
            Orders = new ObservableCollection<Order>(orderList);
        }
        
        private void LoadCustomers()
        {
            var customerList = _customerService.GetCustomers();
            Customers = new ObservableCollection<Customer>(customerList);
        }
        
        private void LoadEmployees()
        {
            var employeeList = _employeeService.GetEmployees();
            Employees = new ObservableCollection<Employee>(employeeList);
        }
        
        private void LoadProducts()
        {
            var productList = _productService.GetProducts();
            Products = new ObservableCollection<Product>(productList);
        }
        
        // For now, we'll just implement the basic operations
        // The actual UI and detailed functionality will need to be expanded
        
        private void ExecuteAdd(object parameter)
        {
            // Get a default employee for required member
            var defaultEmployee = Employees.FirstOrDefault() ?? new Employee { EmployeeID = 1, Name = "Default Employee", UserName = "default", Password = "123", JobTitle = "Staff" };
            
            SelectedOrder = new Order
            {
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>(),
                Employee = defaultEmployee,
                EmployeeID = defaultEmployee.EmployeeID
            };
            
            // Clear order details for new order
            OrderDetails.Clear();
            
            // Reset form fields
            SelectedCustomer = null;
            SelectedEmployee = null;
            SelectedProduct = null;
            Quantity = 1;
            Discount = 0;
            
            IsEditing = true;
        }
        
        private bool CanExecuteEdit(object parameter)
        {
            return SelectedOrder != null && !IsEditing;
        }
        
        private void ExecuteEdit(object parameter)
        {
            IsEditing = true;
        }
        
        private bool CanExecuteDelete(object parameter)
        {
            return SelectedOrder != null && !IsEditing;
        }
        
        private void ExecuteDelete(object parameter)
        {
            if (SelectedOrder != null)
            {
                _orderService.DeleteOrder(SelectedOrder.OrderID);
                Orders.Remove(SelectedOrder);
                SelectedOrder = null;
            }
        }
        
        private bool CanExecuteSave(object parameter)
        {
            return IsEditing && SelectedOrder != null &&
                   SelectedCustomer != null &&
                   SelectedEmployee != null &&
                   OrderDetails.Count > 0;
        }
        
        private void ExecuteSave(object parameter)
        {
            if (SelectedOrder != null && SelectedCustomer != null && SelectedEmployee != null)
            {
                // Set customer and employee
                SelectedOrder.CustomerID = SelectedCustomer.CustomerID;
                SelectedOrder.Customer = SelectedCustomer;
                SelectedOrder.EmployeeID = SelectedEmployee.EmployeeID;
                SelectedOrder.Employee = SelectedEmployee;
                
                // Set order details
                SelectedOrder.OrderDetails = OrderDetails.ToList();
                
                if (SelectedOrder.OrderID == 0)
                {
                    // New order
                    _orderService.AddOrder(SelectedOrder);
                    Orders.Add(SelectedOrder);
                }
                else
                {
                    // Existing order
                    _orderService.UpdateOrder(SelectedOrder);
                    
                    // Refresh the list
                    LoadOrders();
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
            IsEditing = false;
            LoadOrders(); // Reload to get original data
        }
        
        private void ExecuteFilterByDate(object parameter)
        {
            // For now, just a simple example
            // In the real implementation, we'd have date pickers in the UI
            var startDate = DateTime.Now.AddDays(-30); // Last 30 days
            var endDate = DateTime.Now;
            
            var filteredOrders = _orderService.GetOrdersByDateRange(startDate, endDate);
            Orders = new ObservableCollection<Order>(filteredOrders);
        }
        
        private bool CanExecuteAddProductToOrder(object parameter)
        {
            return IsEditing && SelectedProduct != null && Quantity > 0;
        }
        
        private void ExecuteAddProductToOrder(object parameter)
        {
            if (SelectedProduct != null && Quantity > 0)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = SelectedProduct.ProductID,
                    Product = SelectedProduct,
                    UnitPrice = SelectedProduct.UnitPrice,
                    Quantity = Quantity,
                    Discount = Discount / 100 // Convert percentage to decimal
                };
                
                OrderDetails.Add(orderDetail);
                
                // Reset fields
                SelectedProduct = null;
                Quantity = 1;
                Discount = 0;
                
                // Refresh save command
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        
        private bool CanExecuteRemoveProductFromOrder(object parameter)
        {
            return IsEditing && SelectedOrderDetail != null;
        }
        
        private void ExecuteRemoveProductFromOrder(object parameter)
        {
            if (SelectedOrderDetail != null)
            {
                OrderDetails.Remove(SelectedOrderDetail);
                SelectedOrderDetail = null;
                
                // Refresh save command
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
    }
} 
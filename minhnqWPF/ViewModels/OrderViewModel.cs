using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;

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
        
        public OrderViewModel()
        {
            _orderService = new OrderService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _employeeService = new EmployeeService();
            LoadOrders();
            
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteCancel);
            FilterByDateCommand = new RelayCommand(ExecuteFilterByDate);
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
        
        private void LoadOrders()
        {
            var orderList = _orderService.GetOrders();
            Orders = new ObservableCollection<Order>(orderList);
        }
        
        // For now, we'll just implement the basic operations
        // The actual UI and detailed functionality will need to be expanded
        
        private void ExecuteAdd(object parameter)
        {
            // We need to ensure we have an employee to assign to the order
            var employees = _employeeService.GetEmployees();
            if (employees.Count == 0)
            {
                // If no employees exist, create at least one
                _employeeService.GenerateSampleDataset();
                employees = _employeeService.GetEmployees();
            }
            
            var employee = employees[0];
            
            SelectedOrder = new Order
            {
                OrderDate = DateTime.Now,
                OrderDetails = new System.Collections.Generic.List<OrderDetail>(),
                Employee = employee,
                EmployeeID = employee.EmployeeID
            };
            
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
                   SelectedOrder.CustomerID > 0 &&
                   SelectedOrder.EmployeeID > 0 &&
                   SelectedOrder.OrderDetails != null &&
                   SelectedOrder.OrderDetails.Count > 0;
        }
        
        private void ExecuteSave(object parameter)
        {
            if (SelectedOrder != null)
            {
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
    }
} 
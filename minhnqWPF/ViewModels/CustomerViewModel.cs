using System.Collections.ObjectModel;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;
using System.Linq;

namespace minhnqWPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers = new();
        private Customer? _selectedCustomer;
        private string _searchText = string.Empty;
        private bool _isEditing;
        
        public CustomerViewModel()
        {
            _customerService = new CustomerService();
            LoadCustomers();
            
            AddCommand = new RelayCommand(ExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel, CanExecuteCancel);
            SearchCommand = new RelayCommand(ExecuteSearch);
        }
        
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }
        
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set 
            {
                if (SetProperty(ref _selectedCustomer, value))
                {
                    // Refresh command availability
                    ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
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
        
        private void LoadCustomers()
        {
            // First try to get from service
            var customerList = _customerService.GetCustomers();
            
            // If empty, generate sample data directly
            if (customerList.Count == 0)
            {
                _customerService.GenerateSampleDataset();
                customerList = _customerService.GetCustomers();
            }
            
            // If still empty, create some test data directly
            if (customerList.Count == 0)
            {
                customerList = new List<Customer>
                {
                    new Customer { CustomerID = 1, CompanyName = "ABC Technology Corp", ContactName = "Nguyễn Văn An", ContactTitle = "Giám đốc", Address = "123 Lê Lợi, Q1, TP.HCM", Phone = "0901234567", Password = "123456" },
                    new Customer { CustomerID = 2, CompanyName = "XYZ Trading Ltd", ContactName = "Trần Thị Bình", ContactTitle = "Trưởng phòng kinh doanh", Address = "456 Nguyễn Huệ, Q1, TP.HCM", Phone = "0912345678", Password = "123456" },
                    new Customer { CustomerID = 3, CompanyName = "DEF Solutions", ContactName = "Lê Văn Cường", ContactTitle = "CEO", Address = "789 Điện Biên Phủ, Q3, TP.HCM", Phone = "0923456789", Password = "123456" }
                };
            }
            
            Customers = new ObservableCollection<Customer>(customerList);
            
            // Debug: Check if data is loaded
            System.Diagnostics.Debug.WriteLine($"Loaded {customerList.Count} customers");
        }
        
        private void ExecuteAdd(object parameter)
        {
            SelectedCustomer = new Customer
            {
                CompanyName = string.Empty,
                ContactName = string.Empty,
                ContactTitle = string.Empty,
                Address = string.Empty,
                Phone = string.Empty,
                Password = "123456" // Default password
            };
            IsEditing = true;
        }
        
        private bool CanExecuteEdit(object parameter)
        {
            return SelectedCustomer != null && !IsEditing;
        }
        
        private void ExecuteEdit(object parameter)
        {
            if (SelectedCustomer != null)
            {
                // Create a copy for editing
                SelectedCustomer = new Customer
                {
                    CustomerID = SelectedCustomer.CustomerID,
                    CompanyName = SelectedCustomer.CompanyName,
                    ContactName = SelectedCustomer.ContactName,
                    ContactTitle = SelectedCustomer.ContactTitle,
                    Address = SelectedCustomer.Address,
                    Phone = SelectedCustomer.Phone,
                    Password = SelectedCustomer.Password
                };
                IsEditing = true;
            }
        }
        
        private bool CanExecuteDelete(object parameter)
        {
            return SelectedCustomer != null && !IsEditing;
        }
        
        private void ExecuteDelete(object parameter)
        {
            if (SelectedCustomer != null)
            {
                _customerService.DeleteCustomer(SelectedCustomer.CustomerID);
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }
        
        private bool CanExecuteSave(object parameter)
        {
            return IsEditing && SelectedCustomer != null &&
                   !string.IsNullOrEmpty(SelectedCustomer.CompanyName) &&
                   !string.IsNullOrEmpty(SelectedCustomer.ContactName) &&
                   !string.IsNullOrEmpty(SelectedCustomer.Phone);
        }
        
        private void ExecuteSave(object parameter)
        {
            if (SelectedCustomer != null)
            {
                if (SelectedCustomer.CustomerID == 0)
                {
                    // New customer
                    _customerService.AddCustomer(SelectedCustomer);
                    Customers.Add(SelectedCustomer);
                }
                else
                {
                    // Existing customer
                    _customerService.UpdateCustomer(SelectedCustomer);
                    
                    // Refresh the list
                    LoadCustomers();
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
            if (SelectedCustomer != null && SelectedCustomer.CustomerID == 0)
            {
                // If we were adding a new customer, clear selection
                SelectedCustomer = null;
            }
            
            IsEditing = false;
            LoadCustomers(); // Reload to get original data
        }
        
        private void ExecuteSearch(object parameter)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                LoadCustomers();
            }
            else
            {
                var results = _customerService.SearchCustomers(SearchText);
                Customers = new ObservableCollection<Customer>(results);
            }
        }
    }
} 
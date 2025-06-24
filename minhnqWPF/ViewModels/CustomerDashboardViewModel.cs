using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using minhnqWPF.Views;
using Services;

namespace minhnqWPF.ViewModels
{
    public class CustomerDashboardViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private Customer _customer;
        private ObservableCollection<Order> _orders = new();
        private bool _isEditingProfile;

        public CustomerDashboardViewModel(Customer customer)
        {
            _customerService = new CustomerService();
            _orderService = new OrderService();
            _customer = customer;
            
            LoadOrders();
            
            LogoutCommand = new RelayCommand(ExecuteLogout);
            EditProfileCommand = new RelayCommand(ExecuteEditProfile);
            SaveProfileCommand = new RelayCommand(ExecuteSaveProfile, CanExecuteSaveProfile);
            CancelEditCommand = new RelayCommand(ExecuteCancelEdit);
        }

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        public bool IsEditingProfile
        {
            get => _isEditingProfile;
            set 
            { 
                if (SetProperty(ref _isEditingProfile, value))
                {
                    // Refresh command availability
                    ((RelayCommand)SaveProfileCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand LogoutCommand { get; }
        public ICommand EditProfileCommand { get; }
        public ICommand SaveProfileCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadOrders()
        {
            var orderList = _orderService.GetOrdersByCustomerID(Customer.CustomerID);
            Orders = new ObservableCollection<Order>(orderList);
        }

        private void ExecuteLogout(object parameter)
        {
            // Show login window and close this window
            LoginView loginView = new LoginView();
            loginView.Show();
            
            // Close the current window
            if (parameter is Window window)
            {
                window.Close();
            }
        }

        private void ExecuteEditProfile(object parameter)
        {
            IsEditingProfile = true;
        }

        private bool CanExecuteSaveProfile(object parameter)
        {
            return IsEditingProfile &&
                   !string.IsNullOrEmpty(Customer.CompanyName) &&
                   !string.IsNullOrEmpty(Customer.ContactName) &&
                   !string.IsNullOrEmpty(Customer.Phone);
        }

        private void ExecuteSaveProfile(object parameter)
        {
            _customerService.UpdateCustomer(Customer);
            IsEditingProfile = false;
            
            MessageBox.Show("Your profile has been updated successfully.", 
                "Profile Updated", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteCancelEdit(object parameter)
        {
            // Reload customer data to revert changes
            Customer = _customerService.GetCustomerByID(Customer.CustomerID) ?? Customer;
            IsEditingProfile = false;
        }
    }
} 
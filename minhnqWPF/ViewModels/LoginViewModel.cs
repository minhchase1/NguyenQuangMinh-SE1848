using System;
using System.Windows;
using System.Windows.Input;
using minhnqWPF.Commands;
using BusinessObjects;
using Services;
using minhnqWPF.Views;

namespace minhnqWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;
        
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isEmployee = true;

        public LoginViewModel()
        {
            _employeeService = new EmployeeService();
            _customerService = new CustomerService();
            _employeeService.GenerateSampleDataset();
            _customerService.GenerateSampleDataset();
            
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public bool IsEmployee
        {
            get { return _isEmployee; }
            set { SetProperty(ref _isEmployee, value); }
        }

        public ICommand LoginCommand { get; }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                bool isValidLogin = false;
                
                if (IsEmployee)
                {
                    isValidLogin = _employeeService.ValidateEmployee(Username, Password);
                    if (isValidLogin)
                    {
                        var employee = _employeeService.GetEmployeeByUsername(Username);
                        
                        // Navigate to admin dashboard
                        AdminDashboardView adminView = new AdminDashboardView();
                        adminView.Show();
                        
                        // Close the login window
                        if (parameter is Window loginWindow)
                        {
                            loginWindow.Close();
                        }
                        else
                        {
                            // Try to get the login window from Application.Current
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window is LoginView)
                                {
                                    window.Close();
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Customer login using phone number
                    isValidLogin = _customerService.ValidateCustomer(Username, Password);
                    if (isValidLogin)
                    {
                        var customer = _customerService.GetCustomerByPhone(Username);
                        
                        if (customer != null)
                        {
                            // Navigate to customer dashboard
                            CustomerDashboardView customerView = new CustomerDashboardView(customer);
                            customerView.Show();
                            
                            // Close the login window
                            if (parameter is Window loginWindow)
                            {
                                loginWindow.Close();
                            }
                            else
                            {
                                // Try to get the login window from Application.Current
                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window is LoginView)
                                    {
                                        window.Close();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorMessage = "Customer not found. Please try again.";
                        }
                    }
                }

                if (!isValidLogin)
                {
                    ErrorMessage = "Invalid username or password. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error during login: {ex.Message}";
            }
        }
    }
} 
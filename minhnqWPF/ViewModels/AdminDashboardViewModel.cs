using System.Windows;
using System.Windows.Input;
using minhnqWPF.Commands;
using minhnqWPF.Views;

namespace minhnqWPF.ViewModels
{
    public class AdminDashboardViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel = new CustomerViewModel();

        public AdminDashboardViewModel()
        {
            // Initialize commands
            NavigateCommand = new RelayCommand(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        private void ExecuteNavigate(object parameter)
        {
            string? destination = parameter as string;
            switch (destination)
            {
                case "Customers":
                    CurrentViewModel = new CustomerViewModel();
                    break;
                case "Products":
                    CurrentViewModel = new ProductViewModel();
                    break;
                case "Orders":
                    CurrentViewModel = new OrderViewModel();
                    break;
                case "Reports":
                    CurrentViewModel = new ReportViewModel();
                    break;
            }
        }

        private void ExecuteLogout(object parameter)
        {
            // Show login window and close this window
            LoginView loginView = new LoginView();
            loginView.Show();
            
            // Close the current window
            if (parameter is Window currentWindow)
            {
                currentWindow.Close();
            }
            else
            {
                // Try to get the window from Application.Current
                foreach (Window win in Application.Current.Windows)
                {
                    if (win is AdminDashboardView)
                    {
                        win.Close();
                        break;
                    }
                }
            }
        }
    }
} 
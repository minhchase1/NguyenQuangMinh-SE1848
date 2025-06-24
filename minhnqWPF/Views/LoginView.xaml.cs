using System.Windows;
using System.Windows.Controls;
using minhnqWPF.ViewModels;

namespace minhnqWPF.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            
            // Pass the password from PasswordBox to ViewModel since it can't be bound directly
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
} 
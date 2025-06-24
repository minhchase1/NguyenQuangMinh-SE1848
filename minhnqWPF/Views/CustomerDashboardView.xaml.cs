using System.Windows;
using BusinessObjects;
using minhnqWPF.ViewModels;

namespace minhnqWPF.Views
{
    public partial class CustomerDashboardView : Window
    {
        public CustomerDashboardView(Customer customer)
        {
            InitializeComponent();
            DataContext = new CustomerDashboardViewModel(customer);
        }
    }
} 
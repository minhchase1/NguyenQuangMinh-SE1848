using System;
using System.Windows;
using minhnqWPF.ViewModels;

namespace minhnqWPF.Views
{
    public partial class AdminDashboardView : Window
    {
        public AdminDashboardView()
        {
            InitializeComponent();
            DataContext = new AdminDashboardViewModel();
        }
    }
} 
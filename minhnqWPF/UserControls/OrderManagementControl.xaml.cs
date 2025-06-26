using System.Windows.Controls;
using minhnqWPF.ViewModels;

namespace minhnqWPF.UserControls
{
    public partial class OrderManagementControl : UserControl
    {
        public OrderManagementControl()
        {
            InitializeComponent();
            DataContext = new OrderViewModel();
        }
    }
} 
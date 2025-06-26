using System.Windows.Controls;
using minhnqWPF.ViewModels;

namespace minhnqWPF.UserControls
{
    public partial class CustomerManagementControl : UserControl
    {
        public CustomerManagementControl()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel();
        }
    }
} 
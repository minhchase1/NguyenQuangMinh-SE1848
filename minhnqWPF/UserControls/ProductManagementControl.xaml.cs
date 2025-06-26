using System.Windows.Controls;
using minhnqWPF.ViewModels;

namespace minhnqWPF.UserControls
{
    public partial class ProductManagementControl : UserControl
    {
        public ProductManagementControl()
        {
            InitializeComponent();
            DataContext = new ProductViewModel();
        }
    }
} 
using System.Windows.Controls;
using minhnqWPF.ViewModels;

namespace minhnqWPF.UserControls
{
    public partial class ReportsControl : UserControl
    {
        public ReportsControl()
        {
            InitializeComponent();
            DataContext = new ReportViewModel();
        }
    }
} 
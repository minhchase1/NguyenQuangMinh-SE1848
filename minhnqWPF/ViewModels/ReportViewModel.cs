using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;

namespace minhnqWPF.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;
        private ObservableCollection<Order> _orders = new();
        private DateTime _startDate;
        private DateTime _endDate;
        private decimal _totalSales;
        private int _orderCount;
        
        public ReportViewModel()
        {
            _orderService = new OrderService();
            
            // Default to last 30 days
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            
            GenerateReportCommand = new RelayCommand(ExecuteGenerateReport);
            
            // Generate initial report
            ExecuteGenerateReport(null);
        }
        
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        
        public decimal TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }
        
        public int OrderCount
        {
            get => _orderCount;
            set => SetProperty(ref _orderCount, value);
        }
        
        public ICommand GenerateReportCommand { get; }
        
        private void ExecuteGenerateReport(object? parameter)
        {
            var orderList = _orderService.GetOrdersByDateRange(StartDate, EndDate)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
                
            Orders = new ObservableCollection<Order>(orderList);
            OrderCount = orderList.Count;
            
            // Calculate total sales
            decimal total = 0;
            foreach (var order in orderList)
            {
                if (order.OrderDetails != null)
                {
                    foreach (var detail in order.OrderDetails)
                    {
                        total += detail.GetTotal();
                    }
                }
            }
            
            TotalSales = total;
        }
    }
} 
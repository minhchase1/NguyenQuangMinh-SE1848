using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BusinessObjects;
using minhnqWPF.Commands;
using Services;
using System.Collections.Generic;

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
        private int _totalCustomers;
        private int _totalProducts;
        
        public ReportViewModel()
        {
            _orderService = new OrderService();
            
            // Data is already initialized in App.xaml.cs
            
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
        
        public int TotalOrders => OrderCount;
        
        public int TotalCustomers
        {
            get => _totalCustomers;
            set => SetProperty(ref _totalCustomers, value);
        }
        
        public int TotalProducts
        {
            get => _totalProducts;
            set => SetProperty(ref _totalProducts, value);
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
            
            // Calculate other statistics
            var customerService = new CustomerService();
            var productService = new ProductService();
            
            TotalCustomers = customerService.GetCustomers().Count;
            TotalProducts = productService.GetProducts().Count;
            
            // Generate detailed reports
            GenerateDetailedReports(orderList);
        }
        
        private void GenerateDetailedReports(List<Order> orders)
        {
            // Sales by Customer
            var salesByCustomer = orders
                .Where(o => o.Customer != null)
                .GroupBy(o => o.Customer)
                .Select(g => new SalesByCustomerReport
                {
                    CustomerName = g.Key?.CompanyName ?? "Unknown",
                    TotalOrders = g.Count(),
                    TotalAmount = g.Sum(o => o.OrderDetails?.Sum(od => od.GetTotal()) ?? 0)
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();
            
            SalesByCustomer = new ObservableCollection<SalesByCustomerReport>(salesByCustomer);
            
            // Sales by Product
            var salesByProduct = orders
                .SelectMany(o => o.OrderDetails ?? new List<OrderDetail>())
                .Where(od => od.Product != null)
                .GroupBy(od => od.Product)
                .Select(g => new SalesByProductReport
                {
                    ProductName = g.Key?.ProductName ?? "Unknown",
                    QuantitySold = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.GetTotal())
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();
                
            SalesByProduct = new ObservableCollection<SalesByProductReport>(salesByProduct);
            
            // Monthly Sales
            var monthlySales = orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new MonthlySalesReport
                {
                    Month = $"{g.Key.Month:00}/{g.Key.Year}",
                    TotalOrders = g.Count(),
                    TotalSales = g.Sum(o => o.OrderDetails?.Sum(od => od.GetTotal()) ?? 0)
                })
                .OrderBy(x => x.Month)
                .ToList();
                
            MonthlySales = new ObservableCollection<MonthlySalesReport>(monthlySales);
        }
        
        // Report properties
        private ObservableCollection<SalesByCustomerReport> _salesByCustomer = new();
        private ObservableCollection<SalesByProductReport> _salesByProduct = new();
        private ObservableCollection<MonthlySalesReport> _monthlySales = new();
        
        public ObservableCollection<SalesByCustomerReport> SalesByCustomer
        {
            get => _salesByCustomer;
            set => SetProperty(ref _salesByCustomer, value);
        }
        
        public ObservableCollection<SalesByProductReport> SalesByProduct
        {
            get => _salesByProduct;
            set => SetProperty(ref _salesByProduct, value);
        }
        
        public ObservableCollection<MonthlySalesReport> MonthlySales
        {
            get => _monthlySales;
            set => SetProperty(ref _monthlySales, value);
        }
    }
    
    // Report classes
    public class SalesByCustomerReport
    {
        public string CustomerName { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalAmount { get; set; }
    }
    
    public class SalesByProductReport
    {
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
    
    public class MonthlySalesReport
    {
        public string Month { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
    }
} 
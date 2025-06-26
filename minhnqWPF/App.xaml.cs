using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Services;

namespace minhnqWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Initialize all services with sample data
        InitializeServices();
    }
    
    private void InitializeServices()
    {
        // Initialize all services with sample data
        var customerService = new CustomerService();
        var productService = new ProductService();
        var categoryService = new CategoryService();
        var employeeService = new EmployeeService();
        var orderService = new OrderService();
        
        customerService.GenerateSampleDataset();
        productService.GenerateSampleDataset();
        categoryService.GenerateSampleDataset();
        employeeService.GenerateSampleDataset();
        orderService.GenerateSampleDataset();
        
        // Debug: Check if data was initialized
        var customers = customerService.GetCustomers();
        System.Diagnostics.Debug.WriteLine($"App startup: Initialized {customers.Count} customers");
    }
}


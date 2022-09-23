using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.Interfaces.Customers;
using OnlineFoodOrdering.Service.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineFoodOrdering.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        private readonly ICustomerService _customerService;
        public CustomersPage()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var customers = await _customerService.GetAllAsync();
            //foreach (var customer in customers)
            //    Console.WriteLine($"{customer.Id}");
            dtGrid.ItemsSource = customers.ToList();
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DeleteBtn(object sender, RoutedEventArgs e)
        {
            var custom = (Customer)dtGrid.SelectedItem;

            var isDelete = await _customerService.DeleteAsync(p => p.Id == custom.Id);

            if (isDelete is true)
            {
                var items = await _customerService.GetAllAsync();
                dtGrid.ItemsSource = items;

                MessageBox.Show("Xaridor ochirildi!", "Success!");
            }
            else
                MessageBox.Show("Xaridor ochirilmadi!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}

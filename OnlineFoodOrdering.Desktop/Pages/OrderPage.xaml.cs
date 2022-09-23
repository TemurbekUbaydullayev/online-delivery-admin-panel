using OnlineFoodOrdering.Desktop.Windows;
using OnlineFoodOrdering.Service.DTOs.Orders;
using OnlineFoodOrdering.Service.Interfaces.Orders;
using OnlineFoodOrdering.Service.Services.Orders;
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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private IOrderService _orderService;
        public OrderPage()
        {
            InitializeComponent();
            _orderService = new OrderService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var orders = await _orderService.GetAllAsync();

            dtGrid.ItemsSource = orders;
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {

        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DeleteBtn(object sender, RoutedEventArgs e)
        {
            var order = (OrderViewModel)dtGrid.SelectedItem;

            var isDelete = await _orderService.DeleteAsync(p => p.Id == order.Id);
            if (isDelete is true)
            {
                var orders = await _orderService.GetAllAsync();
                dtGrid.ItemsSource = orders;

                MessageBox.Show("Buyurtma ochirildi!", "Success!");
            }
            else
                MessageBox.Show("Buyurtma ochirilmadi!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void ViewBtn(object sender, RoutedEventArgs e)
        {
            var order = (OrderViewModel)dtGrid.SelectedItem;            
            OrderDetailWindow orderDetailWindow = new OrderDetailWindow();
            HiddenGrid.Visibility = Visibility.Visible;
            orderDetailWindow.InputId(order.Id);
            orderDetailWindow.ShowDialog();
            HiddenGrid.Visibility = Visibility.Collapsed;
        }

    }
}

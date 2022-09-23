using OnlineFoodOrdering.Desktop.Pages;
using OnlineFoodOrdering.Service.Interfaces.Orders;
using OnlineFoodOrdering.Service.Services.Orders;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace OnlineFoodOrdering.Desktop.Windows
{
    
    /// <summary>
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        private  long _viewId;
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailWindow()
        {
            InitializeComponent();
            _orderDetailService = new OrderDetailService();
        }

        public void InputId(long id)
        {
            _viewId = id;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var orderDetails = (await _orderDetailService.GetAllAsync(p => p.OrderId == _viewId));
            dtGrid.ItemsSource = orderDetails;   
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

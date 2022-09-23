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
using System.Windows.Shapes;

namespace OnlineFoodOrdering.Desktop
{
    /// <summary>
    /// Interaction logic for Main_2_Window.xaml
    /// </summary>
    public partial class Main_2_Window : Window
    {
        public Main_2_Window(bool isEmployee = false)
        {
            InitializeComponent();

            if (isEmployee)
            {
                // ScarletUI.Visibility = Visibility.Collapsed;
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdFoods_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/FoodsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        //private void rdProducts_Checked(object sender, RoutedEventArgs e)
        //{
        //    PagesNavigation.Navigate(new Uri("Pages/ProductsPage.xaml", UriKind.RelativeOrAbsolute));

        //}
        private void rdCategories_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/CategoriesPage.xaml", UriKind.RelativeOrAbsolute));

        }
       
        private void rdEmployees_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/EmployeesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdCustomers_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/CustomersPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdInvalidProducts_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/InvalidProductsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdFoods_Checked(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/FoodsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PagesNavigation_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void rdOrders_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new Uri("Pages/OrderPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }
    }

}

using OnlineFoodOrdering.Desktop.Windows;
using OnlineFoodOrdering.Domain.Entities;
using OnlineFoodOrdering.Service.DTOs.Foods;
using OnlineFoodOrdering.Service.Interfaces.Foods;
using OnlineFoodOrdering.Service.Services.Foods;
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
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class FoodsPage : Page
    {
        IFoodService foodService = new FoodService();
        public FoodsPage()
        {
            InitializeComponent();
        }
        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            var foods = await foodService.GetAllAsync();
            dtGrid.ItemsSource = foods;
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void DeleteBtn(object sender, RoutedEventArgs e)
        {
            var food = (FoodViewModel)dtGrid.SelectedItem;

            var isDelete = await foodService.DeleteAsync(p => p.Id == food.Id);

            if (isDelete is true)
            {
                var items = await foodService.GetAllAsync();
                dtGrid.ItemsSource = items;

                MessageBox.Show("Mahsulot ochirildi!", "Success!");
            }
            else
                MessageBox.Show("Mahsulot ochirilmadi!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void UpdateBtn(object sender, RoutedEventArgs e)
        {
                FoorUpdateWindow foorUpdateWindow = new FoorUpdateWindow();
            // HiddenGrid -> bu FoodCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
                HiddenGrid.Visibility = Visibility.Visible;
                var foodType = (FoodViewModel)dtGrid.SelectedItem;
                foorUpdateWindow.InputId((int)foodType.Id);
                foorUpdateWindow.ShowDialog();
                HiddenGrid.Visibility = Visibility.Collapsed;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FoodCreateWindow foodCreateWindow = new FoodCreateWindow();
            // HiddenGrid -> bu FoodCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
            HiddenGrid.Visibility = Visibility.Visible; 
            foodCreateWindow.ShowDialog();
            HiddenGrid.Visibility = Visibility.Collapsed; 

        }

        private void addBtn(object sender, RoutedEventArgs e)
        {
            FoodCreateWindow foodCreateWindow = new FoodCreateWindow();
            // HiddenGrid -> bu FoodCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
            HiddenGrid.Visibility = Visibility.Visible;
            foodCreateWindow.ShowDialog();
            HiddenGrid.Visibility = Visibility.Collapsed;
        }
    }

}

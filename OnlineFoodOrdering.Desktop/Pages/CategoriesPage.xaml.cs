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
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    /// 
    
    public partial class CategoriesPage : Page
    {
        private readonly IFoodTypeService foodTypeService;

        public CategoriesPage()
        {
            InitializeComponent();
            foodTypeService = new FoodTypeService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var categories = await foodTypeService.GetAllAsync();

            dtGrid.ItemsSource = categories.ToList();
        }

        public async void Refresh()
        {
            var categories = await foodTypeService.GetAllAsync();

            dtGrid.ItemsSource = categories.ToList();

            
        }

        private void AddBtn(object sender, RoutedEventArgs e)
        {
            CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
            // HiddenGrid -> bu categoryCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
            HiddenGrid.Visibility = Visibility.Visible;
            categoryCreateWindow.ShowDialog();
            HiddenGrid.Visibility = Visibility.Collapsed;
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DeleteBtn(object sender, RoutedEventArgs e)
        {
            var foodType = (FoodType)dtGrid.SelectedItem;

            var isDelete = await foodTypeService.DeleteAsync(p => p.Id == foodType.Id);

            if (isDelete is true)
            {
                var items = await foodTypeService.GetAllAsync();
                dtGrid.ItemsSource = items.ToList();

                MessageBox.Show("Categoriya ochirildi!", "Success!");
            }
            else
                MessageBox.Show("Categoriya ochirilmadi!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {

            CategoryUpdateWindow categoryUpdateWindow = new CategoryUpdateWindow();
            // HiddenGrid -> bu FoodCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
            HiddenGrid.Visibility = Visibility.Visible;
            var foodType = (FoodType)dtGrid.SelectedItem;
            categoryUpdateWindow.InputId((int)foodType.Id); 
            categoryUpdateWindow.ShowDialog();
            HiddenGrid.Visibility = Visibility.Collapsed;
        }

        //private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}


        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void DeleteBtn(object sender, RoutedEventArgs e)
        //{

        //}
        //private void AddBtn(object sender, RoutedEventArgs e)
        //{
        //    FoodCreateWindow foodCreateWindow = new FoodCreateWindow();
        //    // HiddenGrid -> bu FoodCreateWindow chaqirilganda ostidagi windowni xira qilib beradi
        //    HiddenGrid.Visibility = Visibility.Visible;
        //    foodCreateWindow.ShowDialog();
        //    HiddenGrid.Visibility = Visibility.Collapsed;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    IList<Categor> categorList = new List<Categor>()
        //    {
        //        new Categor()
        //        {
        //            Id = 1,
        //            Name = "Ichimlik",
        //            Description = "Ajoyib"
        //        }
        //    };
        //}

        //private void UpdateBtn(object sender, RoutedEventArgs e)
        //{

        //}

    }
}

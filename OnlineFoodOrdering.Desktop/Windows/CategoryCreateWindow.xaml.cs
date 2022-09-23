using OnlineFoodOrdering.Desktop.Pages;
using OnlineFoodOrdering.Service.DTOs.Foods;
using OnlineFoodOrdering.Service.Interfaces.Foods;
using OnlineFoodOrdering.Service.Services.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CategoryCreateWindow.xaml
    /// </summary>
    public partial class CategoryCreateWindow : Window
    {
        private readonly IFoodTypeService foodTypeService;
        public CategoryCreateWindow()
        {
            InitializeComponent();
            foodTypeService = new FoodTypeService();
        }

        private void productPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");

            //e.Handled = regex.IsMatch(e.Text);
        }

        private async void SaveClickBtn(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(productName.Text) || string.IsNullOrEmpty(categoryDescription.Text))
            {
                MessageBox.Show("Bo'sh satrlarni to'ldiring!");
                return;
            }

            FoodTypeForCreationDto foodTypeForCreationDto = new FoodTypeForCreationDto()
            {
                Name = productName.Text,
                Description = categoryDescription.Text,
            };

            var createFoodType = await foodTypeService.CreateAsync(foodTypeForCreationDto);
            //MessageBox.Show(isCreate.ToString());
            if (createFoodType is not null)
            {
                CategoriesPage categories = new CategoriesPage();
                var items = await foodTypeService.GetAllAsync();
                categories.Refresh();

                MessageBox.Show("Categoriya Yaratildi!", "Success!");
            }
            else
                MessageBox.Show("Categoriya yaratilmadi!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);



            if (createFoodType is not null)
            {
                DialogResult = true;
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Categoriya yaratilmadi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                productName.Text = String.Empty;
                categoryDescription.Text = String.Empty;
            }

            // foodService.create 
            // code 
        }

        private void CancelClickBtn(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

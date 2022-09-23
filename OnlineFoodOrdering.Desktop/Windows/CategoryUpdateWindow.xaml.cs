using OnlineFoodOrdering.Domain.Entities;
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
    /// Interaction logic for CategoryUpdateWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        private readonly IFoodTypeService _foodTypeService;
        private int _foodTypeId;
        public CategoryUpdateWindow()
        {
            InitializeComponent();
            _foodTypeService = new FoodTypeService();
        }

        private void productPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");

            //e.Handled = regex.IsMatch(e.Text);
        }

        public void InputId(int id)
        {
            _foodTypeId = id;
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

            var foodType = await _foodTypeService.UpdateAsync(_foodTypeId,foodTypeForCreationDto);

            productName.Text = string.Empty;
            categoryDescription.Text = string.Empty;

            MessageBox.Show("Categoriya Yaratildi!", "Success!");

            DialogResult = true;
            this.Close();
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

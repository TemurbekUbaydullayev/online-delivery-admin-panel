using Microsoft.Win32;
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
    /// Interaction logic for FoorUpdateWindow.xaml
    /// </summary>
    public partial class FoorUpdateWindow : Window
    {
        private readonly IFoodService _foodService;
        private readonly IFoodTypeService _foodTypeService;
        private int _foodId;
        public FoorUpdateWindow()
        {
            InitializeComponent();
            _foodService = new FoodService();
            _foodTypeService = new FoodTypeService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var select = await _foodTypeService.GetAllAsync();

            //foreach (var category in categories)
            //    select.Add(category.Name);

            selectCategory.ItemsSource = select.ToList();
        }
        public void InputId(int id)
        {
            _foodId = id;
        }
        private async void SaveClickBtn(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(productName.Text) || string.IsNullOrEmpty(selectCategory.Text) || string.IsNullOrEmpty(productPrice.Text) || string.IsNullOrEmpty(PhotoPathLabel.Content.ToString()))
            {
                MessageBox.Show("Bo'sh satrlarni to'ldiring!");
                return;
            }


            var foodType = await _foodTypeService.GetAsync(p => p.Name == selectCategory.Text);

            FoodForCreationDto foodForCreationDto = new FoodForCreationDto()
            {
                Name = productName.Text,
                Price = float.Parse(productPrice.Text),
                FoodTypeId = foodType.Id,
                ImagePath = PhotoPathLabel.Content.ToString()!
            };

            var createType = await _foodService.UpdateAsync(_foodId, foodForCreationDto);
            //MessageBox.Show(isCreate.ToString());
            if (createType is not null)
            {
                FoodsPage foods = new FoodsPage();
                var items = await _foodService.GetAllAsync();

                MessageBox.Show("Yangi taom qo'shildi!", "Success!");
            }
            else
                MessageBox.Show("Yangi taom qo'shilmadi!!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);



            if (createType is not null)
            {
                DialogResult = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("Yangi taom qo'shilmadi!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                productName.Text = String.Empty;
                productPrice.Text = String.Empty;
                selectCategory.Text = string.Empty;
                selectImage.Name = string.Empty;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*"
            };

            var result = ofd.ShowDialog();

            PhotoPathLabel.Content = result == true ? ofd.FileName : "Choose a photo";
        }

        private void CancelClickBtn(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}

using Admin.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        Product contextProduct;
        public AddProductPage(Product product)
        {
            InitializeComponent();
            if (product.Id != 0)
            {
                TBName.IsEnabled = false;
                CBAnimal.IsEnabled = false;
                CBTypeProduct.IsEnabled = false;
            }
            CBAnimal.ItemsSource = App.DB.Animal.ToList();
            CBTypeProduct.ItemsSource = App.DB.ProductType.ToList();
            contextProduct = product;
            DataContext = contextProduct;
        }

        private void BEditImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextProduct.Image = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextProduct;
            }
        }

        private void Digits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextProduct.Name) == true)
            {
                errorMessage += "Введите название\n";
            }
            if (contextProduct.Cost == 0 || contextProduct.Cost < 0)
            {
                errorMessage += "Введите корректную цену\n";
            }
            if (contextProduct.ProductType == null)
            {
                errorMessage += "Выберите категорию\n";
            }
            if (contextProduct.Animal == null)
            {
                errorMessage += "Выберите животное\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }


            if (contextProduct.Id == 0)
            {
                App.DB.Product.Add(contextProduct);
                MessageBox.Show($"Товар {contextProduct.Name} был успешно добавлен");
            }
            else
            {
                MessageBox.Show($"Товар {contextProduct.Name} был успешно изменен");
            }
            App.DB.SaveChanges();
            NavigationService.Navigate(new ProductsListPage());
        }
    }
}

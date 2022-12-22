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
using Admin.Model;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsListPage.xaml
    /// </summary>
    public partial class ProductsListPage : Page
    {
        public ProductsListPage()
        {
            InitializeComponent();
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage(new Product()));
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LVItems.SelectedItem as Product;
            if (selectedItem == null)
            {
                MessageBox.Show("Выберите предмет");
                return;
            }
            NavigationService.Navigate(new AddProductPage(selectedItem));
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            //var selectedAnimal = LVItems.SelectedItem as Animal;
            //if (selectedAnimal == null)
            //{
            //    MessageBox.Show("Выберите предмет");
            //    return;
            //}
            //App.DB.Animal.Remove(selectedAnimal);
            //App.DB.SaveChanges();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (string.IsNullOrWhiteSpace(TBSearch.Text))
            {
                LVItems.ItemsSource = App.DB.Product.ToList();
            }
            else
            {
                LVItems.ItemsSource = App.DB.Product.Where(a => a.Name.ToLower().Contains(TBSearch.Text.ToLower()));
            }
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}

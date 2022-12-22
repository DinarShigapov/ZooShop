using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Manager.Model;

namespace Manager.Pages
{
    /// <summary>
    /// Логика взаимодействия для SupplyPage.xaml
    /// </summary>
    public partial class SupplyPage : Page
    {
        Product product = new Product();
        public SupplyPage()
        {
            InitializeComponent();
            CBProvider.ItemsSource = App.DB.Provider.ToList();
            LVProduct.ItemsSource = App.DB.Product.ToList();
        }

        private void BDelProduct_Click(object sender, RoutedEventArgs e)
        {
            BDelProduct.Visibility = Visibility.Collapsed;
            product = new Product();
            TBProduct.Text = "";
            LVProduct.IsEnabled = true;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            if (product == null)
                return;
            if (CBProvider == null)
                return;
            if (TBQuan.Text == "")
                return;


            var provider = CBProvider.SelectedItem as Provider;

            Supply supply = new Supply
            {
                Provider = provider,
                DateTimeSupply = DateTime.Now
            };

            App.DB.Supply.Add(supply);
            App.DB.SaveChanges();

            CompositionOfSupply cos = new CompositionOfSupply
            {
                Supply = supply,
                Product = product,
                Quantity = short.Parse(TBQuan.Text)
            };
            var buf = App.DB.Stock.FirstOrDefault(x => x.ProductId == product.Id);
            buf.Quantity += short.Parse(TBQuan.Text);

            App.DB.CompositionOfSupply.Add(cos);
            App.DB.SaveChanges();
        }

        private void BSelect_Click(object sender, RoutedEventArgs e)
        {
            TBProduct.Text = ((sender as Button).DataContext as Product).Name;
            product = (sender as Button).DataContext as Product;
            BDelProduct.Visibility = Visibility.Visible;
            LVProduct.IsEnabled = false;
        }

        private void Digits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }
    }
}

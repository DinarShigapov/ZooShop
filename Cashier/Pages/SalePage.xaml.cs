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
using Cashier.Model;

namespace Cashier.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        List<Product> products = new List<Product>();
        public SalePage()
        {
            InitializeComponent();
        }


        private void BSale_Click(object sender, RoutedEventArgs e)
        {
            var selected = (sender as Button).DataContext as Product;
            var buf = App.DB.Stock.FirstOrDefault(x => x.ProductId == selected.Id);
            if (buf == null)
                return;

            var query = products.GroupBy(x => x)
              .Where(g => g.Count() >= 1)
              .ToDictionary(x => x.Key, y => y.Count());

            if (query.FirstOrDefault(x => x.Key.Id == selected.Id).Value == buf.Quantity ||
                buf.Quantity <= 0)
            {
                MessageBox.Show("Этого товара нет в наличии");
                return;
            }


            products.Add(selected);
            LVBasket.ItemsSource = products.ToList();
            RefreshBonus();
        }

        private void RefreshBonus()
        {
            decimal bonus = 0;
            foreach (var item in products)
            {
                bonus += item.Cost / 10;
            }
            CountBonus.Text = bonus.ToString();
        }

        private void BDelItem_Click(object sender, RoutedEventArgs e)
        {
            var selected = (sender as Button).DataContext as Product;
            products.Remove(selected);
            LVBasket.ItemsSource = products.ToList();
            RefreshBonus();
        }

        private void BSaleProducts_Click(object sender, RoutedEventArgs e)
        {
            if (products.Count == 0)
            {
                MessageBox.Show("В корзине нет товаров");
                return;
            }
            var bonusCard = App.DB.BonusCard.FirstOrDefault(x => x.NumberCard == TBCard.Text);
            Payment payment = new Payment
            {
                Employee = App.LoggedEmployee,
                BonusCard = bonusCard,
                DateTimeSale = DateTime.Now
            };
            if (bonusCard != null)
            {
                bonusCard.Bonus += decimal.Parse(CountBonus.Text);
            }

            var query = products.GroupBy(x => x)
              .Where(g => g.Count() >= 1)
              .ToDictionary(x => x.Key, y => y.Count());


            foreach (var item in query)
            {
                Sale sale = new Sale
                {
                    Product = item.Key,
                    Payment = payment,
                    Quantity = (short)item.Value

                };
                var buf = App.DB.Stock.FirstOrDefault(x => x.ProductId == item.Key.Id);
                buf.Quantity -= (short)item.Value;
                payment.Sale.Add(sale);
            }


            var messageBuffer = MessageBox.Show("Оформить доставку?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (messageBuffer == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new DeliveryPage(payment));
            }
            else if(messageBuffer == MessageBoxResult.No)
            { 
                App.DB.Payment.Add(payment);
                try
                {
                    App.DB.SaveChanges();
                    MessageBox.Show("Успешная покупку");
                    NavigationService.Navigate(new SalePage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (string.IsNullOrWhiteSpace(TBSearch.Text))
            {
                LVProduct.ItemsSource = App.DB.Product.ToList();
            }
            else
            {
                LVProduct.ItemsSource =
                    App.DB.Product.Where(
                        a => a.Name.ToString().Contains(TBSearch.Text.ToLower())
                        || a.ProductType.Name.ToString().Contains(TBSearch.Text.ToLower())).ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}

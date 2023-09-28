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
using Cashier.Classes;
using Cashier.Model;

namespace Cashier.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        List<ProductQuantity> valueProduct = new List<ProductQuantity> ();
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

            var productBuffer = valueProduct.FirstOrDefault(x => x.Product.Id == selected.Id);

            if (buf.Quantity <= 0)
            {
                MessageBox.Show("Этого товара нет в наличии");
                return;
            }

            if (productBuffer == null)
            {
                valueProduct.Add(new ProductQuantity(selected, 1));
            }
            else
            {
                productBuffer.Quantity += 1;
            }

            //var query = products.GroupBy(x => x)
            //  .Where(g => g.Count() >= 1)
            //  .ToDictionary(x => x.Key, y => y.Count());


           
            LVBasket.ItemsSource = valueProduct.ToList();
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
            if (valueProduct.Count == 0)
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



            foreach (var item in valueProduct)
            {
                Sale sale = new Sale
                {
                    Product = item.Product,
                    Payment = payment,
                    Quantity = (short)item.Quantity

                };
                var buf = App.DB.Stock.FirstOrDefault(x => x.ProductId == item.Product.Id);
                buf.Quantity -= (short)item.Quantity;
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

        private void BMinus_Click(object sender, RoutedEventArgs e)
        {
            var basketBuffer = (sender as Button).DataContext as ProductQuantity;
            if (basketBuffer == null)
            {
                return;
            }
            if (basketBuffer.Quantity != 1)
            {
                basketBuffer.Quantity -= 1;
            }
            LVBasket.ItemsSource = valueProduct.ToList();
            Refresh();
        }

        private void BPlus_Click(object sender, RoutedEventArgs e)
        {
            var basketBuffer = (sender as Button).DataContext as ProductQuantity;
            if (basketBuffer == null)
            {
                return;
            }
            if (App.DB.Stock.FirstOrDefault(x => x.ProductId == basketBuffer.Product.Id)?.Quantity != basketBuffer.Quantity)
            {
                basketBuffer.Quantity += 1;
            }
            LVBasket.ItemsSource = valueProduct.ToList();
            Refresh();
        }

        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            var basket = (sender as Button).DataContext as ProductQuantity;
            if (basket == null)
            {
                return;
            }
            valueProduct.Remove(basket);
            LVBasket.ItemsSource = valueProduct.ToList();
            Refresh();
        }
    }
}

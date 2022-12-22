using Cashier.Model;
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

namespace Cashier.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoOrderPage.xaml
    /// </summary>
    public partial class InfoOrderPage : Page
    {
        Delivery contextDelivery;
        public InfoOrderPage(Delivery delivery)
        {
            InitializeComponent();
            contextDelivery = delivery;
            DataContext = contextDelivery;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            LVProducts.ItemsSource = App.DB.Sale.Where(x => x.PaymentId == contextDelivery.PaymentId).ToList();
        }
    }
}

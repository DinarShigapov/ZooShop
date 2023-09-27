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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            MenuFrame.Navigate(new SalePage());
        }

        private void MISale_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new SalePage());
        }

        private void MICard_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new CardActivationPage());
        }

        private void MIOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new OrdersListPage());
        }
    }
}

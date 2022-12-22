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
    /// Логика взаимодействия для OrdersListPage.xaml
    /// </summary>
    public partial class OrdersListPage : Page
    {
        public OrdersListPage()
        {
            InitializeComponent();
            LVOrder.ItemsSource = App.DB.Delivery.ToList();
        }

        private void BInfo_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = LVOrder.SelectedItem as Delivery;
            if (selectedOrder == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            NavigationService.Navigate(new InfoOrderPage(selectedOrder));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var buf = App.DB.Delivery.ToList();
            foreach (var item in buf)
            {
                if (item.DTDelivery <= DateTime.Now)
                {
                    item.StatusId = 3;
                }
            }
            App.DB.SaveChanges();
        }
    }
}

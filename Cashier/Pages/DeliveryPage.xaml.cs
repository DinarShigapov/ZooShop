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
    /// Логика взаимодействия для DeliveryPage.xaml
    /// </summary>
    public partial class DeliveryPage : Page
    {
        Delivery contextDelivery;
        public DeliveryPage(Delivery delivery)
        {
            InitializeComponent();
            contextDelivery = delivery;
            DataContext = contextDelivery;
        }

        private void BArrange_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = DPDate.SelectedDate;
            var selectedTime = TBTime.Text;
            if (string.IsNullOrWhiteSpace(contextDelivery.Address))
            {
                MessageBox.Show("Введите адрес");
                return;
            }
            if (selectedDate == null)
            {
                MessageBox.Show("выберите дату");
                return;
            }
            if (TimeSpan.TryParse(selectedTime, out TimeSpan resultTime) == false)
            {
                MessageBox.Show("введите корректное время");
                return;
            }
            contextDelivery.DTDelivery = selectedDate.Value.Add(resultTime);
            App.DB.Delivery.Add(contextDelivery);
            App.DB.SaveChanges();
            NavigationService.Navigate(new SalePage());
        }
    }
}

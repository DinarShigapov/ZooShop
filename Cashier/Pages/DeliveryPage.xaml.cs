using Cashier.Model;
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
            DPDate.Text = $"{DateTime.Now}";
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
                MessageBox.Show("Выберите дату");
                return;
            }
            if (TimeSpan.TryParse(selectedTime, out TimeSpan resultTime) == false)
            {
                MessageBox.Show("Введите корректное время");
                return;
            }
            contextDelivery.DTDelivery = selectedDate.Value.Add(resultTime);
            if (contextDelivery.DTDelivery <= DateTime.Now)
            {
                MessageBox.Show("Введите корректное время");
                return;
            }
            App.DB.Delivery.Add(contextDelivery);
            App.DB.SaveChanges();
            NavigationService.Navigate(new SalePage());
        }

        private void Digits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void TBTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TBTime.MaxLength = 5;

            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ":") && (!TBTime.Text.Contains(":") && TBTime.Text.Length != 0)))
            {
                e.Handled = true;
            }
            if (e.Text == ":" && (TBTime.Text.Length > 2 || TBTime.Text.Length < 2))
            {
                e.Handled = true;
            }
            if (TBTime.Text.Length >= 4 && TBTime.Text[2].ToString() != ":")
            {
                e.Handled = true;
            }
        }

        private void TBTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TBTime.Text.Length == 4)
            {
                TBTime.Text = $"{TBTime.Text[0]}{TBTime.Text[1]}:{TBTime.Text[2]}{TBTime.Text[3]}";
            }
        }
    }
}

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
        Delivery contextDelivery = new Delivery();
        public DeliveryPage(Payment payment)
        {
            InitializeComponent();
            contextDelivery.Payment = payment;
            contextDelivery.StatusId = 1;
            DPDate.Text = $"{DateTime.Now}";
            DataContext = contextDelivery;
            LVProducts.ItemsSource = contextDelivery.Payment.Sale.ToList();
        }

        private void BArrange_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = DPDate.SelectedDate;
            var selectedTime = new TimeSpan
                (
                    int.Parse(TBTimeHour.Text.ToString()),
                    int.Parse(TBTimeMinute.Text.ToString()),
                    0
                );
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

            contextDelivery.DTDelivery = new DateTime(

                    selectedDate.Value.Year,
                    selectedDate.Value.Month,
                    selectedDate.Value.Day,
                    selectedTime.Hours,
                    selectedTime.Minutes,
                    0
                );

            if (contextDelivery.DTDelivery <= DateTime.Now)
            {
                MessageBox.Show("Введите корректное время");
                return;
            }

            try
            {
                App.DB.Delivery.Add(contextDelivery);
                App.DB.SaveChanges();
                NavigationService.Navigate(new SalePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
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
            var textBox = sender as TextBox;

            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
                return;
            }

            if (textBox == TBTimeMinute
                && int.Parse($"{textBox.Text}{e.Text}") >= 60)
            {
                e.Handled = true;
            }

            if (textBox == TBTimeHour
                && int.Parse($"{textBox.Text}{e.Text}") > 19)
            {
                e.Handled = true;
            }
        }

        private void TBTimeMinute_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBTimeMinute.Text == "")
            {
                TBTimeMinute.Text = "00";
            }
            else if (int.Parse(TBTimeMinute.Text) < 10 && int.Parse(TBTimeMinute.Text) != 0)
                TBTimeMinute.Text = "0" + TBTimeMinute.Text;

        }

        private void TBTimeHour_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBTimeHour.Text == "")
            {
                TBTimeHour.Text = "00";
            }
            else if (int.Parse(TBTimeHour.Text) < 10 && int.Parse(TBTimeHour.Text) != 0
                     && TBTimeHour.Text.Length != 2)
                TBTimeHour.Text = "0" + TBTimeHour.Text;
        }
    }
}

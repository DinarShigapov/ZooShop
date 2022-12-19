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
using Manager.Model;

namespace Manager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProvidersPage.xaml
    /// </summary>
    public partial class ProvidersPage : Page
    {
        Provider contextProvider = new Provider();
        public ProvidersPage()
        {
            InitializeComponent();
            DataContext = contextProvider;
        }


        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextProvider.LastName) == true)
            {
                errorMessage += "Введите фамилию\n";
            }
            if (string.IsNullOrWhiteSpace(contextProvider.FirstName) == true)
            {
                errorMessage += "Введите имя\n";
            }
            if (string.IsNullOrWhiteSpace(contextProvider.Patronymic) == true)
            {
                errorMessage += "Введите отчество\n";
            }
            if (string.IsNullOrWhiteSpace(contextProvider.Phone) == true)
            {
                errorMessage += "Введите Телефон\n";
            }
            if (string.IsNullOrWhiteSpace(contextProvider.Email) == true)
            {
                errorMessage += "Введите Email\n";
            }
            if (string.IsNullOrWhiteSpace(contextProvider.Address) == true)
            {
                errorMessage += "Введите Адрес\n";
            }


            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            App.DB.Provider.Add(contextProvider);
            App.DB.SaveChanges();
            MessageBox.Show($"Новый поставщик " +
                $"{contextProvider.LastName} " +
                $"{contextProvider.FirstName.ToCharArray()[0]}. " +
                $"{contextProvider.Patronymic.ToCharArray()[0]}. был успешно добавлен");
            NavigationService.Navigate(new ProvidersPage());
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

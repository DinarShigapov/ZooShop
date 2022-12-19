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
    /// Логика взаимодействия для CardActivationPage.xaml
    /// </summary>
    public partial class CardActivationPage : Page
    {
        BonusCard contextCard = new BonusCard();
        public CardActivationPage()
        {
            InitializeComponent();
            Random random = new Random();
            string buf = "";
            bool res = true;

            do
            {
                buf = random.Next(100000, 199999).ToString();
                if (App.DB.BonusCard.Any(x => x.NumberCard != buf))
                {
                    res = false;
                }

            } while (res);
            contextCard.NumberCard = buf;
            contextCard.Bonus = 0;
            DataContext = contextCard;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextCard.LastName) == true)
            {
                errorMessage += "Введите фамилию\n";
            }
            if (string.IsNullOrWhiteSpace(contextCard.FirstName) == true)
            {
                errorMessage += "Введите имя\n";
            }
            if (string.IsNullOrWhiteSpace(contextCard.Patronymic) == true)
            {
                errorMessage += "Введите отчество\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }



            App.DB.BonusCard.Add(contextCard);
            App.DB.SaveChanges();
            MessageBox.Show($"Новая карта с клиентом " +
                $"{contextCard.LastName} " +
                $"{contextCard.FirstName.ToCharArray()[0]}. " +
                $"{contextCard.Patronymic.ToCharArray()[0]}. была успешно добавлена");
            NavigationService.Navigate(new CardActivationPage());
        }
    }
}

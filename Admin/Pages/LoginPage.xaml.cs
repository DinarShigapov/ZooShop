using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TBLogin.Text != "admin" || PBPassword.Password != "admin")
            {
                MessageBox.Show("Логин или пароль неверный");
                return;
            }
            PasswordWindow passwordWindow = new PasswordWindow();

            if (passwordWindow.ShowDialog() == true)
            {
                if (passwordWindow.Password == "1983")
                    MessageBox.Show("Авторизация пройдена");
                else
                {
                    MessageBox.Show("Неверный Пин-код");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Авторизация не пройдена");
                return;
            }

            NavigationService.Navigate(new MenuPage());
        }
    }
}

using Admin.Model;
using Microsoft.Win32;
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

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        Employee contextEmployee = new Employee();

        public AddEmployeePage()
        {
            InitializeComponent();
            CBPost.ItemsSource = App.DB.Post.ToList();
            DataContext = contextEmployee;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextEmployee.LastName) == true)
            {
                errorMessage += "Введите фамилию\n";
            }
            if (string.IsNullOrWhiteSpace(contextEmployee.FirstName) == true)
            {
                errorMessage += "Введите имя\n";
            }
            if (string.IsNullOrWhiteSpace(contextEmployee.Patronymic) == true)
            {
                errorMessage += "Введите отчество\n";
            }
            if (string.IsNullOrWhiteSpace(contextEmployee.Login) == true)
            {
                errorMessage += "Введите логин\n";
            }
            if (string.IsNullOrWhiteSpace(contextEmployee.Password) == true)
            {
                errorMessage += "Введите пароль\n";
            }
            if (contextEmployee.Post == null)
            {
                errorMessage += "Выберите должность\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            App.DB.Employee.Add(contextEmployee);
            App.DB.SaveChanges();
            MessageBox.Show($"Новый сотрудник " +
                $"{contextEmployee.LastName} " +
                $"{contextEmployee.FirstName.ToCharArray()[0]}. " +
                $"{contextEmployee.Patronymic.ToCharArray()[0]}. был успешно добавлен");
            NavigationService.Navigate(new AddEmployeePage());
        }

        

    }
}

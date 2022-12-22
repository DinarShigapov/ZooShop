using Admin.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        Employee contextEmployee;

        public AddEmployeePage(Employee employee)
        {
            InitializeComponent();
            if (employee.Id != 0)
            {
                TBLastName.IsEnabled = false;
                TBFirstName.IsEnabled = false;
                TBPatronymic.IsEnabled = false;
            }
            CBPost.ItemsSource = App.DB.Post.ToList();
            contextEmployee = employee;
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

            if (contextEmployee.Id == 0)
            {
                App.DB.Employee.Add(contextEmployee);
                MessageBox.Show($"Новый сотрудник " +
                $"{contextEmployee.LastName} " +
                $"{contextEmployee.FirstName.ToCharArray()[0]}. " +
                $"{contextEmployee.Patronymic.ToCharArray()[0]}. был успешно добавлен");
            }
            else 
            {
                MessageBox.Show($"Cотрудник " +
                $"{contextEmployee.LastName} " +
                $"{contextEmployee.FirstName.ToCharArray()[0]}. " +
                $"{contextEmployee.Patronymic.ToCharArray()[0]}. был сохранен");
            }
            App.DB.SaveChanges();
            
            NavigationService.Navigate(new EmployeesListPage());
        }

        private void BEditImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextEmployee.Image = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextEmployee;
            }
        }
    }
}

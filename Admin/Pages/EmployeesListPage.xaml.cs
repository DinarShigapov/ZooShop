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
using Admin.Model;

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesListPage.xaml
    /// </summary>
    public partial class EmployeesListPage : Page
    {
        public EmployeesListPage()
        {
            InitializeComponent();
        }


        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage(new Employee() {IsDelete = false}));
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = LVEmployees.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            NavigationService.Navigate(new AddEmployeePage(selectedEmployee));

        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = LVEmployees.SelectedItem as Employee;
            if (selectedEmployee == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            selectedEmployee.IsDelete = true;
            App.DB.SaveChanges();
            Refresh();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (string.IsNullOrWhiteSpace(TBSearch.Text))
            {
                LVEmployees.ItemsSource = App.DB.Employee.Where(x => x.IsDelete == false).ToList();
            }
            else
            {
                LVEmployees.ItemsSource =
                    App.DB.Employee.Where(x => x.IsDelete == false).Where(
                        a => a.StrFullName.ToString().Contains(TBSearch.Text.ToLower())).ToList();
            }

        }
    }
}

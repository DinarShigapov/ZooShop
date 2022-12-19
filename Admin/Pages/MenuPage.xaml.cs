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

namespace Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void MIProduct_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new AddProductPage());
        }

        private void MIEmployee_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new AddEmployeePage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new AddProductPage());
        }
    }
}

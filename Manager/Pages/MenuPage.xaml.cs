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

namespace Manager.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            MenuFrame.Navigate(new SupplyPage());
        }

        private void MISupply_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new SupplyPage());
        }

        private void MIProvider_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new ProvidersPage());
        }
    }
}

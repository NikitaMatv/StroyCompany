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

namespace StroyCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            if (App.LoggedEmployee.Role_Id == 1)
            {

                BtClient.Visibility = Visibility.Visible;
                BtEmployee.Visibility = Visibility.Visible;
                BtOrder.Visibility = Visibility.Visible;
                BtOrderCompl.Visibility = Visibility.Visible;
                BtEmployeeDel.Visibility = Visibility.Visible;
            }
            if (App.LoggedEmployee.Role_Id == 2)
            {
                BtOrder.Visibility = Visibility.Visible;
                MenuFrame.Navigate(new OrderPage());
            }
            if (App.LoggedEmployee.Role_Id == 3)
            {
                BtEmployee.Visibility = Visibility.Visible;
                BtClient.Visibility = Visibility.Visible;
                BtEmployeeDel.Visibility = Visibility.Visible;
                MenuFrame.Navigate(new EmployeePage());
            }
            if (App.LoggedEmployee.Role_Id == 4)
            {
                BtOrder.Visibility = Visibility.Visible;
                BtOrderCompl.Visibility = Visibility.Visible;
                MenuFrame.Navigate(new OrderPage());
            }
        }

        private void BtOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new OrderPage());
        }

        private void BtEmployee_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new EmployeePage());
        }

        private void BtClient_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new ClientPage());

        }

        private void BtOreerCompl_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new OrderComplitPage());
        }

        private void BtEmployeeDel_Click(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(new EmployeeDelPage());
        }
    }
}

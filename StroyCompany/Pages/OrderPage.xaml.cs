using StroyCompany.Components;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            if(App.LoggedEmployee.Role_Id == 2)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).Where(x => x.Client_Id == App.LoggedEmployee.Id || x.Employee_Id == App.LoggedEmployee.Id).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            else if (App.LoggedEmployee.Role_Id == 4)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
            }
            else
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
                DelBt.Visibility = Visibility.Visible;
            }
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderAddEdit(new Order()));
        }

        private void RedBr_Click(object sender, RoutedEventArgs e)
        {
            var selectedorder = LVOrder.SelectedItem as Order;
            NavigationService.Navigate(new OrderAddEdit(selectedorder));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            if (string.IsNullOrWhiteSpace(TbSelected.Text))
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
            }
            else
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower())
                || a.TypeOreder.Name.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }
        }
        private void DelBt_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVOrder.SelectedItem as Order;
            selectedclient.IsCompl = 1;
            App.DB.SaveChanges();
            Refresh();
            if (App.LoggedEmployee.Role_Id == 2)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).Where(x => x.Employee_Id == App.LoggedEmployee.Id).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            else if (App.LoggedEmployee.Role_Id == 4)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
            }
            else
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
                DelBt.Visibility = Visibility.Visible;
            }
        }
        private void TbSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}

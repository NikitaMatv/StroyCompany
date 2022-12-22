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
            TbBalan.Text = App.LoggedEmployee.Balance.ToString();
            if (App.LoggedEmployee.Role_Id == 2)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl != 1).Where(x => x.Client_Id == App.LoggedEmployee.Id || x.Employee_Id == App.LoggedEmployee.Id).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
                SpBal.Visibility = Visibility.Visible;
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
                
              
            }
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderAddEdit(new Order()));
        }

        private void RedBr_Click(object sender, RoutedEventArgs e)
        {
            var selectedorder = LVOrder.SelectedItem as Order;
            if (selectedorder == null)
            {
                MessageBox.Show("Выберете заказ");
                return;
            }
            NavigationService.Navigate(new OrderAddEdit(selectedorder));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Order> filterService = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
            if (App.LoggedEmployee.Role_Id == 2)
            {
                filterService = App.DB.Order.Where(x => x.IsCompl != 1).Where(x => x.Client_Id == App.LoggedEmployee.Id || x.Employee_Id == App.LoggedEmployee.Id).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            else if (App.LoggedEmployee.Role_Id == 4)
            {
                filterService = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
            }
            else
            {
                filterService = App.DB.Order.Where(x => x.IsCompl != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            if (CbSort.SelectedIndex == 0)
                    
            {
                filterService = filterService.Where(x => x.IsCompl != 1).ToList();
                OplBt.Visibility = Visibility.Collapsed;
                DelBt.Visibility = Visibility.Collapsed;
            }
            if (CbSort.SelectedIndex == 1)
            {
                filterService = filterService.Where(x => x.IsCompl == 2).ToList();
                OplBt.Visibility = Visibility.Collapsed;
                if(App.LoggedEmployee.Role_Id != 2)
                DelBt.Visibility = Visibility.Visible;
            }

            else if (CbSort.SelectedIndex == 2)
            {
                filterService = filterService.Where(x => x.IsCompl != 1 && x.IsCompl != 2).ToList(); 
                DelBt.Visibility = Visibility.Collapsed;
                if(App.LoggedEmployee.Role_Id == 2)
                OplBt.Visibility = Visibility.Visible;
            }
            if (TbSelected.Text.Length > 0)
            {
                filterService = filterService.Where(x => x.IsCompl != 1).Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower())
              || a.TypeOreder.Name.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }
            LVOrder.ItemsSource = filterService.ToList();
          

        }
        private void DelBt_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVOrder.SelectedItem as Order;
            if(selectedclient == null)
            {
                MessageBox.Show("Выберете заказ");
                return;
            }
            if(selectedclient.IsCompl == 2)
            {
                selectedclient.IsCompl = 1;
            }
            else
            {
                MessageBox.Show("Заказ не оплачен");
            }
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

        private void OplBt_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVOrder.SelectedItem as Order;
            if (selectedclient == null)
            {
                MessageBox.Show("Выберете заказ");
                return;
            }
            if (selectedclient.IsCompl == 2)
            {
                MessageBox.Show("Заказ уже оплачен");
            }
            else if (selectedclient.Price <= App.LoggedEmployee.Balance)
            {
                selectedclient.IsCompl = 2;
                App.LoggedEmployee.Balance -= selectedclient.Price;
                App.DB.SaveChanges();
                NavigationService.Navigate(new OrderPage());
               
            }
            else
            {
                MessageBox.Show("Недостаточно средств");
            }
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}

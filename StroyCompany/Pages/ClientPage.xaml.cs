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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            if (App.LoggedEmployee.Role_Id == 3)
            {
                LVClient.ItemsSource = App.DB.Employee.Where(x => x.Role_Id == 2).Where(x => x.IsDel != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            else
            {
                LVClient.ItemsSource = App.DB.Employee.Where(x => x.Role_Id == 2).Where(x => x.IsDel != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
        }
        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientAddEditPage(new Employee()));
        }

        private void RedBr_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVClient.SelectedItem as Employee;
            NavigationService.Navigate(new ClientAddEditPage(selectedclient));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refreh();
        }
        private void Refreh()
        {
            if (string.IsNullOrWhiteSpace(TbSelected.Text))
            {
                LVClient.ItemsSource = App.DB.Employee.Where(x => x.Role_Id == 2).Where(x => x.IsDel != 1).ToList();
            }
            else
            {
                LVClient.ItemsSource = App.DB.Employee.Where(x => x.Role_Id == 2).Where(x => x.IsDel != 1).Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower()) || a.Surname.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }

        }

        private void TbSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refreh();
        }

       
    }
}

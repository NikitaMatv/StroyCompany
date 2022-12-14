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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            if (App.LoggedEmployee.Role_Id == 3)
            {
                LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Id != App.LoggedEmployee.Id && x.Role_Id == 4).Where(x => x.IsDel != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
            }
            else
            {
                 LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id !=2).Where(x => x.IsDel != 1).ToList();
                AddBt.Visibility = Visibility.Visible;
                RedBr.Visibility = Visibility.Visible;
                DelBt.Visibility = Visibility.Visible;
            }
           
        }
        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeAddEdintPages(new Employee()));
        }

        private void RedBr_Click(object sender, RoutedEventArgs e)
        {
            var selectedorder = LVEmployee.SelectedItem as Employee;
            NavigationService.Navigate(new EmployeeAddEdintPages(selectedorder));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refreh();
        }
        private void Refreh()
        {
            if (string.IsNullOrWhiteSpace(TbSelected.Text))
            {
                LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 2).Where(x => x.IsDel != 1).ToList();
            }
            else
            {
                LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 2).Where(x => x.IsDel != 1).Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower()) || a.Surname.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }

        }
        private void DelBt_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVEmployee.SelectedItem as Employee;
            if (selectedclient.Role_Id == 1)
            {
                MessageBox.Show("Директора нельзя уволить");
                return;
            }
            else
            {


                selectedclient.IsDel = 1;
                App.DB.SaveChanges();
                Refreh();
                if (App.LoggedEmployee.Role_Id == 3)
                {
                    LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Id != App.LoggedEmployee.Id && x.Role_Id == 4).Where(x => x.IsDel != 1).ToList();
                    AddBt.Visibility = Visibility.Visible;
                    RedBr.Visibility = Visibility.Visible;
                }
                else
                {
                    LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 2).Where(x => x.IsDel != 1).ToList();
                    AddBt.Visibility = Visibility.Visible;
                    RedBr.Visibility = Visibility.Visible;
                    DelBt.Visibility = Visibility.Visible;
                }
            }
        }
        private void TbSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refreh();
        }

    }
}

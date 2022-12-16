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
    /// Логика взаимодействия для EmployeeDelPage.xaml
    /// </summary>
    public partial class EmployeeDelPage : Page
    {
        public EmployeeDelPage()
        {
            InitializeComponent();
            LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 4).Where(x => x.IsDel == 1).ToList();
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = LVEmployee.SelectedItem as Employee;
            selectedclient.IsDel = null;
            App.DB.SaveChanges();
            LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 4).Where(x => x.IsDel == 1).ToList();

        }
        private void TbSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refreh();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refreh();
        }
        private void Refreh()
        {
            if (string.IsNullOrWhiteSpace(TbSelected.Text))
            {
                LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 2).Where(x => x.IsDel == 1).ToList();
            }
            else
            {
                LVEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id != 2).Where(x => x.IsDel == 1).Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower()) || a.Surname.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }

        }
    }
}

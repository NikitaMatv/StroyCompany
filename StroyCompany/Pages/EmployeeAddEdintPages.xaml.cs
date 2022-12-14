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
using Microsoft.Win32;
using StroyCompany.Components;
using StroyCompany.Pages;
namespace StroyCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAddEdintPages.xaml
    /// </summary>
    public partial class EmployeeAddEdintPages : Page
    {
        Employee employeecontext;
        public EmployeeAddEdintPages(Employee employee)
        {
            InitializeComponent();
            if(App.LoggedEmployee.Role_Id == 1)
            {
                CBRole.ItemsSource = App.DB.Role.ToList();
            }
            else
            if(App.LoggedEmployee.Role_Id == 3)
            {
                CBRole.ItemsSource = App.DB.Role.Where(x => x.Id == 4).ToList();
            }
            else
            {
                CBRole.ItemsSource = App.DB.Role.Where(x => x.Id != 2 && x.Id !=1).ToList();
            }
            employeecontext = employee;
            DataContext = employeecontext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var erormasage = "";
         if(TbLogin.Text == "")
            {
                erormasage += "Заполните логин \n";
            }
            if (TbPassword.Text == "")
            {
                erormasage += "Заполните Пароль \n";
            }
            if (App.DB.Employee.FirstOrDefault(x => x.Password == TbPassword.Text && x.Login == TbLogin.Text) != null)
            {
                erormasage += "Такой пользователь уже есть \n";
            }
            if (TbMeddle_name.Text == "")
            {
                erormasage += "Заполните отчество \n";
            }
            if (TbName.Text == "")
            {
                erormasage += "Заполните имя \n";
            }
            if (TbPhone_number.Text == "")
            {
                erormasage += "Заполните телефон \n";
            }
            if (CBRole.SelectedItem == null)
            {
                erormasage += "Выберите роль \n";
            }
            if (erormasage == "")
            {


                if (employeecontext.Id == 0)
                {
                    App.DB.Employee.Add(employeecontext);
                }
                App.DB.SaveChanges();
                NavigationService.Navigate(new EmployeePage());
            }
            else
            {
                MessageBox.Show(erormasage);
            }
        }
        private void BtImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                employeecontext.Image = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = employeecontext;
            }
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbPhone_number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

using Microsoft.Win32;
using StroyCompany.Components;
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

namespace StroyCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientAddEditPage.xaml
    /// </summary>
    public partial class ClientAddEditPage : Page
    {
        Employee employeecontext;
        public ClientAddEditPage(Employee employee)
        {
            InitializeComponent();
            CBRole.ItemsSource = App.DB.Role.Where(x => x.Id == 2).ToList();
            employeecontext = employee;
            DataContext = employeecontext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var erormasage = "";
            if (TbLogin.Text == "")
            {
                erormasage += "Заполните логин \n";
            }
            if (TbPassword.Text == "")
            {
                erormasage += "Заполните Пароль \n";
            }
            if (TbMeddle_name.Text == "")
            {
                erormasage += "Заполните отчество \n";
            }
            if (TbName.Text == "")
            {
                erormasage += "Заполните имя \n";
            }
            if (App.DB.Employee.FirstOrDefault(x => x.Password == TbPassword.Text && x.Login == TbLogin.Text) != null)
            {
                erormasage += "Такой пользователь уже есть \n";
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
                if (App.LoggedEmployee == null)
                {
                    employeecontext.IsDel = 3;
                    employeecontext.Balance = 0;
                }

                    if (employeecontext.Id == 0)
                {
                    App.DB.Employee.Add(employeecontext);
                }
                App.DB.SaveChanges();
                if(App.LoggedEmployee == null)
                {
                    NavigationService.Navigate(new AuthorizationPage());
                }else
                NavigationService.Navigate(new ClientPage());
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

using Microsoft.Win32;
using StroyCompany.Components;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (TbLogin == null)
            {
                erormasage += "Заполните логин \n";
            }
            if (TbPassword == null)
            {
                erormasage += "Заполните Пароль \n";
            }
            if (TbMeddle_name == null)
            {
                erormasage += "Заполните отчество \n";
            }
            if (TbName == null)
            {
                erormasage += "Заполните имя \n";
            }
            if (TbPhone_number == null)
            {
                erormasage += "Заполните телефон \n";
            }
            if (CBRole.SelectedItem == null)
            {
                erormasage += "Выберите роль \n";
            }
            if(employeecontext.Id == 0)
            {
                App.DB.Employee.Add(employeecontext);
            }
            App.DB.SaveChanges();
            NavigationService.GoBack();
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
    }
}

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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login != null)
                TbLogin.Text = Properties.Settings.Default.Login;
            if (Properties.Settings.Default.Password != null)
                TbPassword.Password = Properties.Settings.Default.Password;
        }

        private void AutorBt_Click(object sender, RoutedEventArgs e)
        {
            var employee = App.DB.Employee.FirstOrDefault(x => x.Login == TbLogin.Text 
            && x.Password == TbPassword.Password && x.IsDel !=1 && x.IsDel !=3);
            if (employee == null)
            {
                MessageBox.Show("Ошибка");
                return;
            }
            if (SaveCb.IsChecked == true)
            {
                Properties.Settings.Default.Login = TbLogin.Text;
                Properties.Settings.Default.Password = TbPassword.Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Password = null;
                Properties.Settings.Default.Save();
            }
            App.LoggedEmployee = employee;
            NavigationService.Navigate(new MainMenu());          
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage(new Employee()));
        }
    }
}

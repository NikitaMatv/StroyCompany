using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Balace.xaml
    /// </summary>
    public partial class Balace : Page
    {
        public Balace()
        {
            InitializeComponent();
            if(App.LoggedEmployee.Balance == null)
            {
                App.LoggedEmployee.Balance = 0;
            }
            App.DB.SaveChanges();
        }

        private void BtPopoln_Click(object sender, RoutedEventArgs e)
        {
            if(TbBalance.Text == "")
            {
                TbBalance.Text = "0";
            }
            App.LoggedEmployee.Balance += int.Parse(TbBalance.Text);
            App.DB.SaveChanges();
            NavigationService.Navigate(new MainMenu());
        }
       

        private void TbBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void BtOtm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }
}

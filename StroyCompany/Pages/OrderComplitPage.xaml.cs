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
    /// Логика взаимодействия для OrderComplitPage.xaml
    /// </summary>
    public partial class OrderComplitPage : Page
    {
        public OrderComplitPage()
        {
            InitializeComponent();
          if(App.LoggedEmployee.Role_Id == 4)
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl == 1).Where(x => x.Employee_Id == App.LoggedEmployee.Id).ToList();
            }
            else
            {
                LVOrder.ItemsSource = App.DB.Order.Where(x => x.IsCompl == 1).ToList();
            }
                              
        }
       
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            if (string.IsNullOrWhiteSpace(TbSelected.Text))
            {
                LVOrder.ItemsSource = App.DB.Order.ToList();
            }
            else
            {
                LVOrder.ItemsSource = App.DB.Order.Where(a => a.Name.ToLower().Contains(TbSelected.Text.ToLower())
                || a.TypeOreder.Name.ToLower().Contains(TbSelected.Text.ToLower())).ToList();
            }
        }
       
        private void TbSelected_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}

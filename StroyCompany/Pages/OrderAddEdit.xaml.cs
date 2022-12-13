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
    /// Логика взаимодействия для OrderAddEdit.xaml
    /// </summary>
    public partial class OrderAddEdit : Page
    {
        Order ordercontext;
        public OrderAddEdit(Order order)
        {
            InitializeComponent();
            CbTypes.ItemsSource = App.DB.TypeOreder.ToList();
            ordercontext = order;
            DataContext = ordercontext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(ordercontext.Name))
            {
                errorMessage += "Введите название\n";
            }
            if (string.IsNullOrWhiteSpace(ordercontext.Address))
            {
                errorMessage += "Введите адрес\n";
            }
            if (ordercontext.Date == null)
            {
                errorMessage += "Введите дату\n";
            }
            if (ordercontext.Description == null)
            {
                errorMessage += "Введите описание\n";
            }
            if (ordercontext.Price == null || ordercontext.Price <= 0)
            {
                errorMessage += "Введите цену\n";
            }
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (ordercontext.Id == 0)
            {
                App.DB.Order.Add(ordercontext);
            }
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }
    }
}

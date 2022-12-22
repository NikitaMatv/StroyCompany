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
    /// Логика взаимодействия для OrderAddEdit.xaml
    /// </summary>
    public partial class OrderAddEdit : Page
    {
        Order ordercontext;
        public OrderAddEdit(Order order)
        {
            InitializeComponent();
            ordercontext = order;
            DataContext = ordercontext;
            CbTypes.ItemsSource = App.DB.TypeOreder.ToList();
            CbEmployee.ItemsSource = App.DB.Employee.Where(x => x.Role_Id == 4).ToList();
            if(App.LoggedEmployee.Role_Id == 2)
                ordercontext.Client_Id = App.LoggedEmployee.Id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (TbName.Text == "")
            {
                errorMessage += "Введите название\n";
            }
            if (TbAdress.Text == "")
            {
                errorMessage += "Введите адрес\n";
            }
            if (DpDate.SelectedDate == null)
            {
                errorMessage += "Введите дату\n";
            }
            if (TbDiscription.Text == "")
            {
                errorMessage += "Введите описание\n";
            }
            if (TbPrise.Text == "")
            {
                errorMessage += "Введите цену\n";
            }
            if (TbPrise.Text.StartsWith("0"))
            {
                errorMessage += "Введите коректную цену\n";
            }
            if (CbTypes.SelectedItem == null)
            {
                errorMessage += "Выберете тип работ\n";
            }
            if (CbEmployee.SelectedItem == null)
            {
                errorMessage += "Выберете отвецтвенного работника\n";
            }
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (errorMessage == "")
            {


                if (ordercontext.Id == 0)
                {
                    App.DB.Order.Add(ordercontext);
                }
                App.DB.SaveChanges();
                NavigationService.Navigate(new OrderPage());
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        private void BtImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                ordercontext.Image = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = ordercontext;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

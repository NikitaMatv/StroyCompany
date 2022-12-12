using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using StroyCompany.Pages;
using StroyCompany.Properties;
using StroyCompany.Components;
namespace StroyCompany

{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static StroyCompanyEntities1 DB = new StroyCompanyEntities1();
        public static Employee LoggedEmployee;
    }
}

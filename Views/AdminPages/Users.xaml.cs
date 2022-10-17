using BusinessManager.Model.Data;
using BusinessManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BusinessManager.Views.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
    {

        public static ListView UsersList;
        public static TextBlock CountofUsers;
        public Users()
        {
           
            InitializeComponent();
            UsersList = AllUsers;
            CountofUsers = CountofU;
        }

       
    }
}

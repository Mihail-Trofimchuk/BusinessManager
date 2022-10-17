
using BusinessManager.Views.AdminPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BusinessManager.Views
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public static Frame Users;
        public Admin()
        {
            InitializeComponent();
            User.NavigationService.Navigate(new Users());
            Users = User;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
           
        }
    }
}

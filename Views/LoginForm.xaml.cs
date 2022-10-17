
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessManager.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            ChangePasswordGrid = ChangePasswordEmail;
            ChangePasswordGridB = ChangePasswordEmailB;
        }

        public static Grid ChangePasswordGrid;
        public static Border ChangePasswordGridB;

        private void AccountModal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangePasswordEmail.Visibility = Visibility.Hidden;
            LoginModal.Visibility = System.Windows.Visibility.Hidden;
            LoginModalUser.Visibility = System.Windows.Visibility.Hidden;
            NewPassword.Visibility = Visibility.Hidden;
            ChangePassword.Visibility = Visibility.Hidden;


        }
    }
}

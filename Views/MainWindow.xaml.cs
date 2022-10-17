
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessManager.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TextBlock Total;
        public static Frame MainFrame;
        public static TextBlock UserN;
        public static Label UserE;

        public MainWindow()
        {
            InitializeComponent();

            MainFrame = PagesNavigation;
            UserN = UserName;
            UserE = UserEmail;
            
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
           
        }
        private void AccountModal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainModal.Visibility = Visibility.Hidden;
            MainModalError.Visibility = Visibility.Hidden;
        }
    }
}




































// создаем два объекта User
//Account BTS = new Account {  Account_Name="BTS" };
//Account Alfa = new Account { Account_Name = "Alfa" };


//db.Account.AddRange(BTS, Alfa);
//db.SaveChanges();
//User tom = new User { Name = "Tom", Account = BTS};
//User bob = new User { Name = "Bob", Account = Alfa };
//User alice = new User { Name = "Alice", Account = BTS };
//User kate = new User { Name = "Kate", Account = Alfa };
//db.Users.AddRange(tom, bob, alice, kate);
//db.SaveChanges();

//// получаем пользователей
//var users = db.Users.ToList();
//foreach (var user in users)
//{

//}

//// Удаляем первую компанию
//var comp = db.Companies.FirstOrDefault();
//db.Companies.Remove(comp);
//db.SaveChanges();
//Console.WriteLine("\nСписок пользователей после удаления компании");
//// снова получаем пользователей
//users = db.Users.ToList();
//foreach (var user in users) Console.WriteLine($"{user.Name}");
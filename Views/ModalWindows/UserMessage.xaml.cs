
using System.Windows;


namespace BusinessManager.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для UserMessage.xaml
    /// </summary>
    public partial class UserMessage : Window
    {
        public UserMessage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}

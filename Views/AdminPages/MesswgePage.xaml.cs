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

namespace BusinessManager.Views.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для MesswgePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        public static ListView Messages;
        public static TextBlock CountofMessages;
        public MessagePage()
        {
            InitializeComponent();
             Messages = MessageList;
            CountofMessages = MesagesCount;
        }
    }
}

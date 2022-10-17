
using System.Windows.Controls;


namespace BusinessManager.Views
{
    /// <summary>
    /// Логика взаимодействия для CounterPartyMenu.xaml
    /// </summary>
    public partial class CounterPartyMenu : Page
    {
        public static TextBox Names;
        public static TextBox FullNames;
        public static TextBox Comments;


        public CounterPartyMenu()
        {
            InitializeComponent();
            Names = Name;
            FullName = FullNames;
            Comment = Comments;

        }
    }
}

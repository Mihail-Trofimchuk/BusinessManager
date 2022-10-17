using BusinessManager.ViewModel;

using System.Windows.Controls;


namespace BusinessManager.Views.Pages.OperationPoint
{
    /// <summary>
    /// Логика взаимодействия для EntrancePage.xaml
    /// </summary>
    public partial class EntrancePage : Page
    {
        public EntrancePage()
        {
            InitializeComponent();
            DataContext = new OperationCommandsViewModel();
        }

     
    }
}


using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace BusinessManager.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
    
        #region
        

        public static Grid OperationGrid;
        public static Frame OperationFrame;

        public static ListView Entrance;
        public static ComboBox Account;
        public static ComboBox Counterparty;
        public static ComboBox Projects;

        public static ListView Payment;
        public static ComboBox AccountP;
        public static ComboBox CounterpartyP;
        public static ComboBox ProjectsP;

        public static TextBlock EntranceCount;
        public static TextBlock PaymentCount;

        public static ComboBox ArticleList;
        public static ComboBox ArticleListP;

        public static Grid OperationEditGrid;
        public static Grid EntranceEditGrid;
        public static Grid PaymentEditGrid;

        #endregion
        public HomePage()
        {
            InitializeComponent();

            #region
            OperationGrid = OperationMenuNav;
            Entrance = EntranceList;
            Account = AccountComboBox;
            Counterparty = CounterpartyComboBox;
            Projects = ProjectCombobox;

            Payment = PaymentP;
            AccountP = AccountPe;
            CounterpartyP = CountrpartyPe;
            ProjectsP = ProjectPe;

            EntranceCount = CountofEntrance;
            PaymentCount = CountofPayment;
            ArticleList = Article;
            ArticleListP = ArticlePList;

            OperationEditGrid = OperationMenuNavEdit;
            EntranceEditGrid = OperationMenuNavEntranceEdit;
            PaymentEditGrid = OperationMenuNavPaymentEdit;

        #endregion

        }

        private void AccountModal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OperationModal.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}


using System.Windows.Controls;


namespace BusinessManager.Views.Pages
{
    public partial class Conterpaty : Page
    {

        public static ListView LegalEntityList;
        public static ListView CounterpartyList;
        public static Frame LegalEntityFrame;
        public static Grid LegalEntity;
        public static Frame CounterPartyFrame;
        public static Grid CounterParty;

        public static Border LegalEntityChange;
        public static Border CounterPartyChange;


        public static TextBlock CountofL;
        public static TextBlock CountofC;

        public Conterpaty()
        {
           
            InitializeComponent();

            LegalEntityList = ViewAllLegalEntity;
            CounterpartyList = ViewAllCounterParty;
            LegalEntity = CounterPartyMenu;
            LegalEntityFrame = CounterPartyMenuNav;
            CounterPartyFrame = CounterPartySecondMenuNav;
            CounterParty = CounterPartySecondMenu;
            CountofL = CountofLegalEntity;
            CountofC = CountofCounterparty;
            LegalEntityChange = LegalEntitychange;
            CounterPartyChange = Counterparychange;
        }
    }
}

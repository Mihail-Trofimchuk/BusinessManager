
using BusinessManager.Views.Pages;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BusinessManager.Model
{
    public static class CurrentPage 
    {
        static public Conterpaty Сounterparty {get;set;}

        static public SoundsPage ProjectsPage { get; set; }

        static public PaymentPage PaymentPage { get; set; }

        static public PaymentPage AccountPage { get; set; }

       
        static public Page CurrentPageTotal { get; set; }
       

        static public ObservableCollection<LegalEntity> AllLegalEntityByIdTry { get; set; }
        static public ObservableCollection<Entrance> AllEntrance { get; set; }
        static public ObservableCollection<Payment> AllPayment { get; set; }

        static public Conterpaty Main { get; set; }

        static public LegalEntity Too { get; set;  }

        static public LegalEntity Nax = new LegalEntity();

      
       
    }
}

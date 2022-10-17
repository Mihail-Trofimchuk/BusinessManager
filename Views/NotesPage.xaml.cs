
using System.Windows.Controls;

namespace BusinessManager.Views.Pages
{

    public partial class NotesPage : Page
    {
        
        public static De.TorstenMandelkow.MetroChart.ChartSeries PieSer;
        public static De.TorstenMandelkow.MetroChart.ChartSeries PieSer2;
        public static De.TorstenMandelkow.MetroChart.ChartSeries  PieSer3;
        public static TextBlock ToTalEntr;
        public static TextBlock TotalPaym;
        public static TextBlock Ranges;
        public static TextBlock Dev;
        public static ComboBox ListofProjects;
       
        public NotesPage()
        {
            InitializeComponent();

            PieSer = Pie;
            PieSer2 = PieSeries2;
            PieSer3 = PieSeries3;
            ToTalEntr = TotalCost;
            TotalPaym = TotalPayment;
            Ranges = Range;
            Dev = Devedents;
            ListofProjects = MainNotesProjects;
        }

       
    }

}
 

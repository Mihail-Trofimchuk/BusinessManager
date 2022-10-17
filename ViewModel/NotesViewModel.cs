using BusinessManager.Model;
using BusinessManager.Model.Data;
using BusinessManager.Views.Pages;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using RelayCommand = BusinessManager.Model.RelayCommand;

namespace BusinessManager.ViewModel
{
    public class NotesViewModel : ViewModelBase
    {


        public NotesViewModel()
        {

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            var r = new Random();

            Values = new Dictionary<string, double>();

            Errors = new ObservableCollection<Entrance>();

            foreach (var item in AllEntranceById)
            {
                Errors.Add(item);
            }

            Errors2 = new ObservableCollection<Payment>();

            foreach (var item in AllPaymentById)
            {
                Errors2.Add(item);
                
            }

            BrushConverter bc = new BrushConverter();

            SeriesCollection = new SeriesCollection();
            LineSeries line = new LineSeries
            {
                Values = new ChartValues<decimal?>(),
                PointGeometrySize = 17,
                StrokeThickness = 4,
                Title = "Поступления",
                Fill = (Brush)bc.ConvertFrom("#4C2196F3")
            };
            
            
            LineSeries line2 = new LineSeries
            {
                Values = new ChartValues<decimal?>(),
                PointGeometrySize = 17,
                StrokeThickness = 4,
                Title = "Выплаты",
                Fill = (Brush)bc.ConvertFrom("#5FF8C8C8")

            };

            ChartValues<decimal> values = new ChartValues<decimal>();
            ChartValues<decimal> values2 = new ChartValues<decimal>();

            foreach (var item in Errors)
            {
                values.Add(item.Entrance_Sum ?? 0);
            }
            foreach (var item in Errors2)
            {
                values2.Add(item.Payment_Sum ?? 0);
            }

            line.Values = values;
            line2.Values = values2;
            SeriesCollection.Add(line);
            SeriesCollection.Add(line2);

            Labels = new List<string>();
            foreach (var item in Errors)
            {
                Labels.Add(item.Entrance_Day.Value.ToShortDateString());
            }
            Labels2 = new List<string>();
            foreach (var item in Errors2)
            {
                Labels2.Add(item.Payment_Day.Value.ToShortDateString());
            }

            RaisePropertyChanged("AllPaymentById");

        }

        public ObservableCollection<Projects> AllProjects { get; private set; } = DataWorker.GetAllProjectsById();
       
        public ObservableCollection<Entrance> Errors { get; private set; } = CurrentPage.AllEntrance;

        public ObservableCollection<Payment> Errors2 { get; private set; } = CurrentPage.AllPayment;
       

        public List<string> Labels { get; set; }
        public List<string> Labels2 { get; set; }
        public Dictionary<string, double> Values { get; set; }
        public Dictionary<string, string> LanguagePack { get; set; }

       

        public static Projects selectedProject { get; set; }
        public Projects SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                RaisePropertyChanged("SelectedProject");
            }
        }

        public SeriesCollection SeriesCollection { get; set; }


        private ObservableCollection<Entrance> _allentranceById = DataWorker.GetAllEntranceById();
        public ObservableCollection<Entrance> AllEntranceById
        {
            get { return _allentranceById; }
            set
            {
                _allentranceById = value;
                RaisePropertyChanged("AllEntranceById");

            }
        }

        private ObservableCollection<Payment> _allpaymentById = DataWorker.GetAllPaymentById();
        public ObservableCollection<Payment> AllPaymentById
        {
            get { return _allpaymentById; }
            set
            {
                _allpaymentById = value;
                RaisePropertyChanged("AllPaymentById");

            }
        }

        // Общая сумма поступлений
        private decimal? totalcost = DataWorker.GetTotalEntranceById();

        public decimal? TotalCost
        {
            get { return totalcost; }
            set
            {
                totalcost = value;
                RaisePropertyChanged("TotalCost");

            }
        }

        // Общая сумма поступлений
        private decimal? totalpayment = DataWorker.GetTotalPaymentById();

        public decimal? Totalpayment
        {
            get { return totalpayment; }
            set
            {
                totalpayment = value;
                RaisePropertyChanged("Totalpayment");

            }
        }
        // Разница сумма поступлений
        private decimal? range = DataWorker.GetRangeById();
        public decimal? Range 
        {
            get { return range; }
            set
            {
                range = value;
                RaisePropertyChanged("Range");

            }
        }
        // Разница сумма поступлений
        private decimal? devedens = DataWorker.GetDevedentsId();
        public decimal? Devedens
        {
            get { return devedens; }
            set
            {
                devedens = value;
                RaisePropertyChanged("Devedens");

            }
        }      

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            var i = 0;
            foreach (var observable in SeriesCollection[2].Values.Cast<ObservableValue>())
            {
                observable.Value = r.Next(0, 10);
                i++;

            }


        }
        public Func<ChartPoint, string> PointLabel { get; set; }

        #region Работа с окном


        private RelayCommand rebuild;
        public RelayCommand Rebuild
        {
            get
            {
                return rebuild ?? new RelayCommand(obj =>
                {
                    if (SelectedProject == null) 
                    {
                        return;
                    }
                    CartesianChart dod = obj as CartesianChart;


                    Errors = DataWorker.GetAllEntranceByIdwithProject(SelectedProject);
                    Errors2 = DataWorker.GetAllPaymentByIdwithProjects(SelectedProject);

                    BrushConverter bc = new BrushConverter();

                    SeriesCollection = new SeriesCollection();
                    LineSeries line = new LineSeries
                    {
                        Values = new ChartValues<decimal?>(),
                        PointGeometrySize = 17,
                        StrokeThickness = 4,
                        Title = "Поступления",
                        Fill = (Brush)bc.ConvertFrom("#4C2196F3")
                    };


                    LineSeries line2 = new LineSeries
                    {
                        Values = new ChartValues<decimal?>(),
                        PointGeometrySize = 17,
                        StrokeThickness = 4,
                        Title = "Выплаты",
                        Fill = (Brush)bc.ConvertFrom("#5FF8C8C8")

                    };

                    ChartValues<decimal> values = new ChartValues<decimal>();
                    ChartValues<decimal> values2 = new ChartValues<decimal>();

                    foreach (var item in Errors)
                    {
                        values.Add(item.Entrance_Sum ?? 0);
                    }
                    foreach (var item in Errors2)
                    {
                        values2.Add(item.Payment_Sum ?? 0);
                    }

                    line.Values = values;
                    line2.Values = values2;
                    SeriesCollection.Add(line);
                    SeriesCollection.Add(line2);

                    Labels = new List<string>();
                    foreach (var item in Errors)
                    {
                        Labels.Add(item.Entrance_Day.Value.ToShortDateString());
                    }
                    Labels2 = new List<string>();
                    foreach (var item in Errors2)
                    {
                        Labels2.Add(item.Payment_Day.Value.ToShortDateString());
                    }
                    dod.Series = SeriesCollection;
                }
                );
            }
        }

        private RelayCommand btn_createoperation;
        public RelayCommand Btn_createoperation
        {
            get
            {
                return btn_createoperation ?? new RelayCommand(obj =>
                {
                    HomePage note = obj as HomePage;
                   
                    MessageBox.Show(note.Name);
                }
                );
            }
        }
       
        #endregion
    }

}
